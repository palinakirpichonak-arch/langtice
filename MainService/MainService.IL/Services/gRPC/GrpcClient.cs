using Grpc.Net.Client;
using MainService.IL.Services.Protos;
using MainService.PL.Services.gRPC;
using Microsoft.Extensions.Logging;

namespace MainService.BLL.Services.gRPC;

public class GrpcClient : IGrpcClient
{
    private readonly ILogger<GrpcClient> _logger;

    public GrpcClient(ILogger<GrpcClient> logger)
    {
        _logger = logger;
    }
    
    public async Task<string> SendMessage(string userId, CancellationToken cancellationToken)
    {
        var request = new EmailRequest()
        {
            UserId = userId
        };
        
        var channel = GrpcChannel.ForAddress("http://auth-service:7071");
        var client = new EmailSender.EmailSenderClient(channel);
        
        var reply = await client.SendEmailAsync(request, cancellationToken: cancellationToken);

        _logger.LogInformation($"Message from server: {reply.Email}");
        
        return reply.Email;
    }
    
}