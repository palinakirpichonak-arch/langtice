using MainService.PL.Services.gRPC;
using Microsoft.AspNetCore.Mvc;

namespace MainService.PL.Features;

[Route("grpc")]
[ApiController]
public class gRPC : ControllerBase
{
    private readonly ILogger<gRPC> _logger;
    private readonly IGrpcClient _client;
    
    public gRPC(ILogger<gRPC> logger,  IGrpcClient client)
    {
        _logger = logger;
        _client = client;
    }
    
    [HttpGet]
    public async Task SendGrpcRequestAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting grpc client");
        
        await _client.SendMessage(cancellationToken);
        
        _logger.LogInformation("Grpc client sent");
    }
}