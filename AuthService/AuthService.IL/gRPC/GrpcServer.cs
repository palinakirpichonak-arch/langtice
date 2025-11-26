using AuthService.DAL.Features.Users.Repositories;
using Grpc.Core;
using AuthService.IL.Protos;
using Microsoft.Extensions.Logging;

namespace AuthService.IL.gRPC;

public class GrpcServer : EmailSender.EmailSenderBase
{
    private readonly ILogger<GrpcServer> _logger;
    private readonly IUserRepository _userRepository;
    
    public GrpcServer(
        ILogger<GrpcServer> logger, 
        IUserRepository userRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
    }

    public override async Task<EmailReply> SendEmail(EmailRequest request, ServerCallContext context)
    {
        var userId = request.UserId;
        var user = await _userRepository.GetByIdAsync(Guid.Parse(userId), context.CancellationToken);
        
        return new EmailReply
        {
            Email =  user.Email
        };
    }

}