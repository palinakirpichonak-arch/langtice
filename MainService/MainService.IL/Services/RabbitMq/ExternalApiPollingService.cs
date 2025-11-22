using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace MainService.BLL.Services.RabbitMq;

public class ExternalApiPollingService : BackgroundService
{
    private readonly ILogger<ExternalApiPollingService> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly TimeSpan _pollingInterval =  TimeSpan.FromSeconds(15);
    
    public ExternalApiPollingService(IHttpClientFactory httpClientFactory, ILogger<ExternalApiPollingService> logger)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }
    
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        
        ConnectionFactory factory = new ConnectionFactory();
        factory.Uri = new Uri("amqp://guest:guest@localhost:5672/");
        factory.HostName = "localhost";
        using var conn = await factory.CreateConnectionAsync(cancellationToken);
        using var channel = await conn.CreateChannelAsync(cancellationToken:cancellationToken);
        
        await channel.QueueDeclareAsync(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null, cancellationToken:cancellationToken);

        var timer = new PeriodicTimer(_pollingInterval);

        while (await timer.WaitForNextTickAsync(cancellationToken))
        {
            try
            {
                var payload = await FetchExternalAsync(cancellationToken);

                var msg = "Message from MainService";

                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(msg));
                
                await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "hello", body: body, cancellationToken: cancellationToken);

                _logger.LogInformation("Published message to RabbitMQ: {Length} bytes", body.Length);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to poll and publish");
            }
        }

    }
    
    private async Task<string> FetchExternalAsync(CancellationToken ct)
    {
        var client = _httpClientFactory.CreateClient("external-api");
        
        var resp = await client.GetAsync("https://example.com/api/status", ct);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadAsStringAsync(ct);
    }
}