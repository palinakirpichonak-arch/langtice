using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Shared.Dto;
using Shared.Options;

namespace MainService.BLL.Services.RabbitMq;

public class RabbitMqPublisher : IRabbitMqPublisher, IAsyncDisposable
{
    private readonly ILogger<RabbitMqPublisher> _logger;
    private readonly RabbitMqOptions _rabbitMqOptions;

    private IConnection? _connection;
    private IChannel? _channel;
    private readonly SemaphoreSlim _sync = new(1, 1);

    public RabbitMqPublisher(
        ILogger<RabbitMqPublisher> logger,
        IOptions<RabbitMqOptions> options)
    {
        _logger = logger;
        _rabbitMqOptions = options.Value;
    }

    private async Task EnsureConnectedAsync(CancellationToken cancellationToken)
    {
        if (_channel is { IsOpen: true })
            return;

        await _sync.WaitAsync(cancellationToken);

        try
        {
            if (_channel is { IsOpen: true })
                return;

            _connection?.Dispose();
            _channel?.Dispose();

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

            _logger.LogInformation("RabbitMQ connected. Queue {Queue} declared.", _rabbitMqOptions.Queue);
        }
        finally
        {
            _sync.Release();
        }
    }

    public async Task PublishAsync(Message message, CancellationToken cancellationToken = default)
    {
        await EnsureConnectedAsync(cancellationToken);

        if (_channel == null)
        {
            _logger.LogError("RabbitMQ channel is null, cannot publish message.");
            return;
        }

        var body = JsonSerializer.SerializeToUtf8Bytes(message);

        await _channel.BasicPublishAsync(
            exchange: "",
            routingKey: _rabbitMqOptions.Queue,
            body: body,
            cancellationToken: cancellationToken);

        _logger.LogInformation(
            "Published notification for user {UserId} ({Email})",
            message.UserId,
            message.Email);
    }

    public async ValueTask DisposeAsync()
    {
        if (_channel != null)
            await _channel.CloseAsync(CancellationToken.None);


        if (_connection != null)
            await _connection.CloseAsync(CancellationToken.None);

        _channel?.Dispose();
        _connection?.Dispose();
        _sync.Dispose();
    }
}