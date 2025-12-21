using MainService.DAL.Models.UserFlashCardModel;
using MainService.DAL.Repositories.UserFlashCards;
using MainService.PL.Services.gRPC;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shared.Data;
using Shared.Dto;

namespace MainService.BLL.Services.RabbitMq;

public class FlashCardNotificationService : BackgroundService
{
    private readonly ILogger<FlashCardNotificationService> _logger;
    private readonly IRabbitMqPublisher _rabbitMqPublisher;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    private readonly PeriodicTimer _checkInterval = new(TimeSpan.FromMinutes(1));
    private readonly TimeSpan _lifeTime = TimeSpan.FromDays(1);

    public FlashCardNotificationService(
        ILogger<FlashCardNotificationService> logger,
        IServiceScopeFactory serviceScopeFactory,
        IRabbitMqPublisher rabbitMqPublisher
    )
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
        _rabbitMqPublisher = rabbitMqPublisher;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Flashcard reminder service started.");

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                await CheckIntervalAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while processing flashcards notifications");
            }

            try
            {
                await Task.Delay(_checkInterval.Period, cancellationToken);
            }
            catch (TaskCanceledException)
            {
                _logger.LogInformation("Flashcard reminder service cancellation requested.");
            }
        }
    }

    private async Task CheckIntervalAsync(CancellationToken cancellationToken)
    {
        var currentTime = DateTime.UtcNow;

        using var scope = _serviceScopeFactory.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<IUserFlashCardsRepository>();
        var grpcClient = scope.ServiceProvider.GetRequiredService<IGrpcClient>();

        var cards = await repo.GetCardsForNotificationAsync(
            _lifeTime,
            _checkInterval.Period,
            currentTime,
            cancellationToken);

        if (cards.Count == 0)
            return;

        _logger.LogInformation("Found {Count} flashcards to notify", cards.Count);

        foreach (var card in cards)
        {
            var expiresAt = card.CreatedAt + _lifeTime;
            var remaining = expiresAt - currentTime;

            if (remaining <= TimeSpan.Zero)
                continue;

            var updateResult = await repo.TryUpdateLastNotificationTimeAsync(
                card.Id,
                card.LastNotificationAt,
                currentTime,
                cancellationToken);

            if (!updateResult)
                continue;

            await SendExpirationNotificationAsync(
                card,
                remaining,
                grpcClient,
                _rabbitMqPublisher,
                cancellationToken);
        }
    }

    private async Task SendExpirationNotificationAsync(
        UserFlashCard card,
        TimeSpan remaining,
        IGrpcClient grpcClient,
        IRabbitMqPublisher publisher,
        CancellationToken cancellationToken)
    {
        try
        {
            var email = await grpcClient.SendMessage(card.UserId.ToString(), cancellationToken);

            if (string.IsNullOrWhiteSpace(email))
            {
                _logger.LogWarning(
                    "Got empty email for user {UserId}. Skipping notification for flashcard {CardId}",
                    card.UserId, card.Id);
                return;
            }
            
            var template = EmailSubjectTemplates
                .Templates[MessageNotificationType.ExpirationNotification];

           var expirationTime = DateTime.UtcNow.Add(remaining);
           var expirationTimeFormatted = expirationTime.ToString("MMMM dd, yyyy HH:mm");

            var msg = new Message
            {
                UserId = card.UserId.ToString(),
                Email = email,
                MessageBody = string.Format(template.MessageBody, expirationTimeFormatted)
            };

            await publisher.PublishAsync(msg, cancellationToken);

            _logger.LogInformation(
                "Sent remaining-time notification for flashcard {Id} to user {UserId}-{UserEmail}. Left: {Remaining}s",
                card.Id, card.UserId, msg.Email, (long)remaining.TotalSeconds);
        }
        catch (OperationCanceledException)
        {
            _logger.LogWarning("Flashcard reminder notification sending was canceled.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error while sending notification for flashcard {CardId} (UserId={UserId})",
                card.Id, card.UserId);
        }
    }
}