using System.Text;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace NotificationService.PL;

public class RabbitConsumerService : BackgroundService
{
    private readonly ILogger<RabbitConsumerService> _logger;
    private readonly RabbitMqOptions _rabbitMqOptions;
    
    public RabbitConsumerService(ILogger<RabbitConsumerService> logger, IOptions<RabbitMqOptions> rabbitMqOptions)
    {
        _logger = logger;
        _rabbitMqOptions = rabbitMqOptions.Value;
    }
    
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = "rabbitmq",
            Port = 5672,
            UserName = "guest",
            Password = "guest"
        };
        
        IConnection? conn = null;
        IChannel? channel = null;

        try
        {
            conn = await factory.CreateConnectionAsync(cancellationToken);
            channel = await conn.CreateChannelAsync(cancellationToken:cancellationToken);

            await channel.QueueDeclareAsync(queue: "notifications-queue", durable: false, exclusive: false, autoDelete: false, arguments: null, cancellationToken:cancellationToken);

            _logger.LogInformation(" [*] Waiting for messages.");

            var consumer = new AsyncEventingBasicConsumer(channel);
        
            consumer.ReceivedAsync += (_, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                _logger.LogInformation(" [x] Received {0}", message);
           
                return Task.CompletedTask;
            };

            await channel.BasicConsumeAsync("notifications-queue", autoAck: true, consumer, cancellationToken);
            try
            {
                await Task.Delay(Timeout.Infinite, cancellationToken);
            }
            catch (TaskCanceledException)
            {
            
            } 
        }
        finally
        {
            channel?.Dispose();
            conn?.Dispose();
        }
    }
}