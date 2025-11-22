using System.Text;
using System.Text.Json;
using MainService.BLL.Options;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace MainService.BLL.Services.RabbitMq;

public class RabbitSenderService : BackgroundService
{
    private readonly ILogger<RabbitSenderService> _logger;
    private readonly RabbitMqOptions _rabbitMqOptions;
    private readonly PeriodicTimer _timer =  new(TimeSpan.FromSeconds(15));
    
    public RabbitSenderService(ILogger<RabbitSenderService> logger,  IOptions<RabbitMqOptions> rabbitMqOptions)
    {
        _logger = logger;
        _rabbitMqOptions = rabbitMqOptions.Value;
    }
    
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        
        while (await _timer.WaitForNextTickAsync(cancellationToken))
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = "rabbitmq",
                    Port = 5672,
                    UserName = "guest",
                    Password = "guest"
                };
        
            using var conn = await factory.CreateConnectionAsync(cancellationToken);
            using var channel = await conn.CreateChannelAsync(cancellationToken:cancellationToken);
            
            await channel.QueueDeclareAsync(
                queue:  "notifications-queue", 
                durable: false, 
                exclusive: false, 
                autoDelete: false, 
                arguments: null, 
                cancellationToken:cancellationToken);
            
                var msg = $"Message from MainService {DateTimeOffset.UtcNow:O}";

                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(msg));
                    
                await channel.BasicPublishAsync(
                    exchange: "", 
                    routingKey:  "notifications-queue", 
                    body: body, 
                    cancellationToken: cancellationToken);

                _logger.LogInformation("Published message to RabbitMQ: {Message}", msg);
            }
            catch (Exception ex)
            {
                    _logger.LogError(ex, "Failed to poll and publish");
            }
        }
    }
}