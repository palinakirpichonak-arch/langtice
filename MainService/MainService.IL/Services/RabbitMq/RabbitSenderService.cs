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
    private IConnection _connection;
    private IChannel _channel;
    
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
                    HostName = _rabbitMqOptions.HostName,
                    Port = _rabbitMqOptions.Port,
                    UserName = _rabbitMqOptions.UserName,
                    Password = _rabbitMqOptions.Password,
                };
                
                _connection = await factory.CreateConnectionAsync(cancellationToken);
                _channel = await _connection.CreateChannelAsync(cancellationToken:cancellationToken);
            
            await _channel.QueueDeclareAsync(
                queue:  _rabbitMqOptions.Queue, 
                durable: true, 
                exclusive: false, 
                autoDelete: false, 
                arguments: null, 
                cancellationToken:cancellationToken);
            
                var msg = $"Message from MainService {DateTimeOffset.UtcNow:O}";

                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(msg));
                    
                await _channel.BasicPublishAsync(
                    exchange:  "", 
                    routingKey:   _rabbitMqOptions.Queue, 
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