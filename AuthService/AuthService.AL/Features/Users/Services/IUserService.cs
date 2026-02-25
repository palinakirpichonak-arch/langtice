using AuthService.AL.Features.Users.Dto;
using AuthService.DAL.Features.Users.Models;

namespace AuthService.AL.Features.Users.Services;

public interface IUserService
{
    Task RegisterUser(RegisterUserDto newUserDto, CancellationToken cancellationToken);
    Task<User?> ValidateCredentialsAsync(LoginUserDto dto, CancellationToken ct);
}