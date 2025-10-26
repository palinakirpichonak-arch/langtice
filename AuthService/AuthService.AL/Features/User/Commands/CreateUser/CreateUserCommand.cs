using MediatR;

namespace AuthService.AL.Features.User.Commands.CreateUser;

public class CreateUserCommand : IRequest<DAL.Users.User>
{
    public DAL.Users.User User { get; set; }
}
