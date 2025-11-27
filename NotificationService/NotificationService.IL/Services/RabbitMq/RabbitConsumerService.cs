using System.Text;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NotificationService.IL.Services.Smtp;
using NotificationService.PL;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace NotificationService.IL.Services.RabbitMq
{
    
public class RabbitConsumerService : BackgroundService
{
    private readonly ILogger<RabbitConsumerService> _logger;
    private readonly RabbitMqOptions _rabbitMqOptions;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private IConnection? _connection;
    private IChannel? _channel;

    public RabbitConsumerService(
        ILogger<RabbitConsumerService> logger, 
        IOptions<RabbitMqOptions> rabbitMqOptions,
        IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _rabbitMqOptions = rabbitMqOptions.Value;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
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

        _logger.LogInformation(" [*] Waiting for messages.");

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += async (_, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            _logger.LogInformation(" [x] Received {0}", message);
          
            using var scope = _serviceScopeFactory.CreateScope();
            var emailSender =  scope.ServiceProvider.GetRequiredService<IEmailSender>();
            
            var fullEmail = JsonSerializer.Deserialize<Message>(message);
            
            await emailSender.SendEmail(fullEmail.Email, "FLASH CARD EXPIRES", fullEmail.MessageBody);
            
        };

        await _channel.BasicConsumeAsync(
            _rabbitMqOptions.Queue, 
            autoAck: true, 
            consumer, 
            cancellationToken);

      
        await Task.Delay(Timeout.Infinite, cancellationToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("RabbitMQ consumer stopping...");
        
        if (_channel != null)
            await _channel.CloseAsync(cancellationToken);
        
        if (_connection != null)
            await _connection.CloseAsync(cancellationToken);

        await base.StopAsync(cancellationToken);
    }

    public override void Dispose()
    {
        _channel?.Dispose();
        _connection?.Dispose();
        base.Dispose();
    }
}
}

public class Message
{
    public string UserId { get; set; }
    public string Email { get; set; }
    public string MessageBody { get; set; }
}
