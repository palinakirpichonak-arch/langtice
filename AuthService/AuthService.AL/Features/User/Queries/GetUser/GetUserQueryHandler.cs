using AuthService.DAL.Abstractions;
using MediatR;

namespace AuthService.AL.Features.User.Queries.GetUser;

public class GetUserQueryHandler :  IRequestHandler<GetUserQuery, DAL.Users.User>
{
    private readonly IUserRepository _userRepository;

    public GetUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<DAL.Users.User> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
       var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

       return user;
    }
}