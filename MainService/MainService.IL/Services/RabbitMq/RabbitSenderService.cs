using System.Text.Json;
using MainService.BLL.Options;
using MainService.DAL.Data.UserFlashCards;
using MainService.DAL.Features.UserFlashCard;
using MainService.PL.Services.gRPC;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace MainService.BLL.Services.RabbitMq;

public class RabbitSenderService : BackgroundService
{
    private readonly ILogger<RabbitSenderService> _logger;
    private readonly RabbitMqOptions _rabbitMqOptions;
    
    private readonly IGrpcClient _grpcClient;
    private IConnection _connection;
    private IChannel _channel;
    private IServiceScopeFactory _serviceScopeFactory;

    private readonly PeriodicTimer _checkInterval = new(TimeSpan.FromMinutes(1));
    private readonly TimeSpan _lifeTime = TimeSpan.FromDays(1);


    public RabbitSenderService(
        IServiceScopeFactory scopeFactory,
        ILogger<RabbitSenderService> logger,
        IOptions<RabbitMqOptions> rabbitMqOptions,
        
        IGrpcClient grpcClient)
    {
        _logger = logger;
        _rabbitMqOptions = rabbitMqOptions.Value;
        _grpcClient = grpcClient;
        _serviceScopeFactory = scopeFactory;
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = _rabbitMqOptions.HostName,
            Port = _rabbitMqOptions.Port,
            UserName = _rabbitMqOptions.UserName,
            Password = _rabbitMqOptions.Password,
        };
        
        _connection = await factory.CreateConnectionAsync(cancellationToken);
        _channel = await _connection.CreateChannelAsync(cancellationToken: cancellationToken);
        
        await _channel.QueueDeclareAsync(
            queue: _rabbitMqOptions.Queue,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            cancellationToken: cancellationToken);
        
        await base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting RabbitMQ client");

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                await CheckInterval(cancellationToken);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error while processing flashcards notifications");
            }
            try
            {
                await Task.Delay(_checkInterval.Period, cancellationToken);
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogError(ex, "Task is cancelled");
            }
        }
    }

    private async Task CheckInterval(CancellationToken cancellationToken)
    {
        var currentTime = DateTime.UtcNow;

        
        using var scope = _serviceScopeFactory.CreateScope();
        
        var repo = scope.ServiceProvider.GetRequiredService<IUserFlashCardsRepository>();
       
        
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
            {
                continue;
            }

            var updateResult =await repo.TryUpdateLastNotificationTimeAsync(
                card.Id,
                card.LastNotificationAt,
                currentTime,
                cancellationToken);

            if (!updateResult)
            {
                continue;
            }

            await SendNotificationAsync(card, remaining, cancellationToken);
        }
    }

    private async Task SendNotificationAsync(
        UserFlashCards card,
        TimeSpan remaining,
        CancellationToken cancellationToken)
    {
        string email = await _grpcClient.SendMessage(card.UserId.ToString(), cancellationToken);

        var msg = new Message()
        {
            UserId = card.UserId.ToString(),
            Email = email,
            MessageBody = "You flashcard expires soon"
        };

        var body = JsonSerializer.SerializeToUtf8Bytes(msg);

        await _channel.BasicPublishAsync(
            exchange: "",
            routingKey: _rabbitMqOptions.Queue,
            body: body,
            cancellationToken: cancellationToken);
        
        _logger.LogInformation(
            "Sent remaining-time notification for flashcards {Id} to user {UserId}-{UserEmail}. Left: {Remaining}s",
            card.Id, card.UserId, msg.Email,(long)remaining.TotalSeconds);
    }
}

public class Message
{
    public string UserId { get; set; }
    public string Email { get; set; }
    public string MessageBody { get; set; }
}