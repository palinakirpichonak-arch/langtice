using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MainService.BLL.Services.RabbitMq;

public class ApiMessagesConsumer : BackgroundService
{
    private readonly ILogger<ApiMessagesConsumer> _logger;
    
    public ApiMessagesConsumer(ILogger<ApiMessagesConsumer> logger)
    {
        _logger = logger;
    }
    
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var factory = new ConnectionFactory(){HostName = "localhost"};
        using var conn = await factory.CreateConnectionAsync();
        using var channel = await conn.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);

        Console.WriteLine(" [*} Waiting for messages.");

        var consumer = new AsyncEventingBasicConsumer(channel);


        consumer.ReceivedAsync += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
           _logger.LogInformation(" [x] Received {0}", message);
            
    
            return Task.CompletedTask;
        };

        await channel.BasicConsumeAsync("hello", autoAck: true, consumer);
    }
}