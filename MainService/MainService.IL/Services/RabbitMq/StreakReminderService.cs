using MainService.DAL.Models.UserStreakModel;
using MainService.DAL.Repositories.UserStreaks;
using MainService.PL.Services.gRPC;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shared.Data;
using Shared.Dto;

namespace MainService.BLL.Services.RabbitMq;

public class StreakReminderService : BackgroundService
{
    private readonly ILogger<StreakReminderService> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IRabbitMqPublisher _rabbitMqPublisher;

    private readonly PeriodicTimer _timer = new(TimeSpan.FromMinutes(1));

    public StreakReminderService(
        ILogger<StreakReminderService> logger,
        IServiceScopeFactory serviceScopeFactory,
        IRabbitMqPublisher rabbitMqPublisher)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
        _rabbitMqPublisher =  rabbitMqPublisher;   
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Streak reminder service started.");

        while (await _timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                await ProcessRemindersAsync(stoppingToken);
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Streak reminder service cancellation requested.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while processing streak reminders.");
            }
        }
    }

    private async Task ProcessRemindersAsync(CancellationToken cancellationToken)
    {
        var nowUtc = DateTime.UtcNow;
        var today = DateOnly.FromDateTime(nowUtc);
        var hour = nowUtc.Hour;
        
        if (hour >= 12 && hour < 18)
            return;

        using var scope = _serviceScopeFactory.CreateScope();
        var streakRepo = scope.ServiceProvider.GetRequiredService<IUserStreakRepository>();
        var grpcClient = scope.ServiceProvider.GetRequiredService<IGrpcClient>();

        List<UserStreak> streaks;

        var isMorningWindow = hour < 12;
        var isEveningWindow = hour >= 18;

        if (isMorningWindow)
        {
            streaks = await streakRepo.GetForMorningReminderAsync(today, nowUtc, cancellationToken);
            _logger.LogInformation("Found {Count} users for morning streak reminder.", streaks.Count);
        }
        else if (isEveningWindow)
        {
            streaks = await streakRepo.GetForEveningReminderAsync(today, nowUtc, cancellationToken);
            _logger.LogInformation("Found {Count} users for evening streak reminder.", streaks.Count);
        }
        else
        {
            return;
        }

        if (streaks.Count == 0)
            return;

        foreach (var streak in streaks)
        {
            try
            {
                var email = await grpcClient.SendMessage(streak.UserId.ToString(), cancellationToken);

                if (string.IsNullOrWhiteSpace(email))
                {
                    _logger.LogWarning("Email for user {UserId} is empty. Skipping streak reminder.", streak.UserId);
                    continue;
                }

                var template = EmailSubjectTemplates.Templates[MessageNotificationType.StreakNotification];

                var msg = new Message
                {
                    UserId = streak.UserId.ToString(),
                    Email = email,
                    MessageBody = template.MessageBody
                };

                await _rabbitMqPublisher.PublishAsync(msg, cancellationToken);

                if (isMorningWindow)
                {
                    await streakRepo.MarkMorningNotifiedAsync(streak.Id, nowUtc, cancellationToken);
                }
                else if (isEveningWindow)
                {
                    await streakRepo.MarkEveningNotifiedAsync(streak.Id, nowUtc, cancellationToken);
                }

                _logger.LogInformation(
                    "Sent streak reminder to user {UserId} ({Email}), streak = {StreakDays} days.",
                    streak.UserId, email, streak.CurrentStreakDays);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error sending streak reminder to user {UserId}", streak.UserId);
            }
        }
    }
}