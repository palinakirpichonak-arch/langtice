using AuthService.AL.Features.Users.Dto;
using AuthService.DAL.Features.Roles.Repositories;
using AuthService.DAL.Features.Users.Models;
using AuthService.DAL.Features.Users.Repositories;
using AuthService.IL.Services;

namespace AuthService.AL.Features.Users.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPasswordHasher _passwordHasher;
    
    public UserService(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _passwordHasher = passwordHasher;
    }
    
    public async Task RegisterUser(RegisterUserDto newUserDto, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByEmailAsync(newUserDto.Email, cancellationToken);
        
        if (existingUser != null)
            throw new Exception("User with this email already exists");
        
        var hashedPassword = _passwordHasher.HashPassword(newUserDto.Password);   
        var user = User.Create(Guid.NewGuid(), newUserDto.Username, newUserDto.Email, hashedPassword, newUserDto.AvatarUrl);
        
        await _userRepository.AddAsync(user,  cancellationToken);
        
        await _roleRepository.AssignUserRolesAsync(user.Id, "User", cancellationToken);
    }
    
    public async Task<User?> ValidateCredentialsAsync(LoginUserDto dto, CancellationToken ct)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email, ct);
        if (user == null)
            return null;

        var ok = _passwordHasher.VerifyHashedPassword(dto.Password, user.PasswordHash);
        if (!ok)
            return null;

        return user;
    }
    
}

