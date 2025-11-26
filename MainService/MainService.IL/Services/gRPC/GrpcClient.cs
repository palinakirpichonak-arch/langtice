using System.IdentityModel.Tokens.Jwt;
using AuthService.PL.Services;
using Grpc.Net.Client;
using MainService.IL.Services.Protos;
using MainService.PL.Services.gRPC;
using Microsoft.Extensions.Logging;

namespace MainService.BLL.Services.gRPC;

public class GrpcClient : IGrpcClient
{
    private readonly ILogger<GrpcClient> _logger;
    private readonly IJwtTokenProvider _jwtTokenProvider;
    private readonly JwtSecurityTokenHandler _tokenHandler;

    public GrpcClient(ILogger<GrpcClient> logger, IJwtTokenProvider jwtTokenProvider)
    {
        _logger = logger;
        _jwtTokenProvider = jwtTokenProvider;
        _tokenHandler =  new JwtSecurityTokenHandler();
    }
    
    public async Task<string> SendMessage(CancellationToken cancellationToken)
    {
        var userId = _tokenHandler.ReadJwtToken(_jwtTokenProvider.GetJwtToken()).Claims.First(x => x.Type == "userId").Value;
        
        var request = new EmailRequest()
        {
            UserId = userId
        };
        
        var channel = GrpcChannel.ForAddress("http://auth-service:7071");
        var client = new EmailSender.EmailSenderClient(channel);
        
        var reply = await client.SendEmailAsync(request);

        _logger.LogInformation($"Message from server: {reply.Email}");
        
        return reply.Email;
    }
    
}