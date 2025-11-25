using AuthService.IL.gRPC;
using Grpc.Net.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MainService.BLL.Services.gRPC;

public class GreeterClient : BackgroundService
{
    private readonly ILogger<GreeterClient> _logger;
    private readonly HelloRequest _helloRequest;
    private readonly Greeter.GreeterClient _client;

    public GreeterClient(ILogger<GreeterClient> logger)
    {
        _logger = logger;
        
        _helloRequest = new HelloRequest() {Name = "Polinka"};
        
        var channel = GrpcChannel.ForAddress("http://auth-service:7071");
        _client = new Greeter.GreeterClient(channel);
        
        _logger.LogInformation($"Setting up gRPC client");
    }
    

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var reply = await _client.SayHelloAsync(_helloRequest);
        
        _logger.LogInformation($"Greeting: {reply.Message}");
    }
}