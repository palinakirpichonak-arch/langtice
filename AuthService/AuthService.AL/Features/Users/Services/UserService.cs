using AuthService.AL.Features.Users.Dto;
using AuthService.DAL.Abstractions;
using AuthService.DAL.Users;
using AuthService.IL.Services;

namespace AuthService.AL.Features.Users.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;
    
    public UserService(
        IUserRepository userRepository, 
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }
    
    public async Task RegisterUser(RegisterUserDto newUserDto, CancellationToken cancellationToken)
    {
        var hashedPassword = _passwordHasher.HashPassword(newUserDto.Password);   
        var user = User.Create(Guid.NewGuid(), newUserDto.Username, newUserDto.Email, hashedPassword, newUserDto.AvatarUrl);
        
        await _userRepository.AddAsync(user,  cancellationToken);
    }

    public async Task<string> Login(LoginUserDto loginUserDto, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(loginUserDto.Email, cancellationToken);

        if (user == null)
        {
            throw new Exception("User not found");
        }
        
        var result = _passwordHasher.VerifyHashedPassword(loginUserDto.Password, user.PasswordHash);

        if (result == false)
        {
            throw new Exception("Invalid username or password");
        }
        
        var token = _jwtProvider.GenerateJwtToken(user);
        
        return token;
    }
    
}

