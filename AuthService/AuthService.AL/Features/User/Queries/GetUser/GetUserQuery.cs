using MediatR;

namespace AuthService.AL.Features.User.Queries.GetUser;

public class GetUserQuery : IRequest<DAL.Users.User>
{
    public Guid Id { get; set; }
}