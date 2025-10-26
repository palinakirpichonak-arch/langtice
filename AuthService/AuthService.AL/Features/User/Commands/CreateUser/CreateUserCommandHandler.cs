using AuthService.DAL.Abstractions;
using MediatR;

namespace AuthService.AL.Features.User.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, DAL.Users.User>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<DAL.Users.User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    { 
        await _userRepository.CreateAsync(request.User, cancellationToken);
        
        return request.User;
    }
}