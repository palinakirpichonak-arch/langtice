using AuthService.AL.Features.Users.Dto;
using AuthService.DAL.Users;

namespace AuthService.AL.Features.Users.Services;

public interface IUserService
{
    Task RegisterUser(RegisterUserDto newUserDto, CancellationToken cancellationToken);
    Task<string> Login(LoginUserDto loginUserDto, CancellationToken cancellationToken);
}