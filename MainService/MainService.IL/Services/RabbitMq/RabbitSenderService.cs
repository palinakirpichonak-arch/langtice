using MainService.DAL.Repositories.UserFlashCards;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Shared.Options;

namespace MainService.BLL.Services.RabbitMq;

public class RabbitSenderService : BackgroundService
{
    private readonly ILogger<RabbitSenderService> _logger;
    private readonly RabbitMqOptions _rabbitMqOptions;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    private IConnection? _connection;
    private IChannel? _channel;

    private readonly PeriodicTimer _checkInterval = new(TimeSpan.FromMinutes(1));
    private readonly TimeSpan _lifeTime = TimeSpan.FromDays(1);

    public RabbitSenderService(
        IServiceScopeFactory scopeFactory,
        ILogger<RabbitSenderService> logger,
        IOptions<RabbitMqOptions> rabbitMqOptions)
    {
        _logger = logger;
        _rabbitMqOptions = rabbitMqOptions.Value;
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
               _logger.LogInformation("RabbitMQ client has been canceled");
            }
        }
    }

    private async Task CheckInterval(CancellationToken cancellationToken)
    {
        if (_channel == null)
        {
            _logger.LogError("RabbitMQ channel is not initialized in CheckInterval");
            return;
        }

        var currentTime = DateTime.UtcNow;

        using var scope = _serviceScopeFactory.CreateScope();
        var repo = scope.ServiceProvider.GetRequiredService<IUserFlashCardsRepository>();
        var messageSender = scope.ServiceProvider.GetRequiredService<IMessageSender>();

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

            await messageSender.SendExpirationNotificationAsync(
                card,
                remaining,
                _channel,
                cancellationToken);
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("RabbitMQ sender stopping...");

        try
        {
            if (_channel != null)
                await _channel.CloseAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error closing RabbitMQ channel");
        }

        try
        {
            if (_connection != null)
                await _connection.CloseAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error closing RabbitMQ connection");
        }

        await base.StopAsync(cancellationToken);
    }

    public override void Dispose()
    {
        _channel?.Dispose();
        _connection?.Dispose();
        base.Dispose();
    }
}
