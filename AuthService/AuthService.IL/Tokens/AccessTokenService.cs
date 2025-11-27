using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthService.DAL.Features.Roles.Repositories;
using AuthService.DAL.Features.Users.Models;
using AuthService.IL.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.IL.Tokens;

public class AccessTokenService :  IAccessTokenService
{
    
    private readonly IOptions<JwtOptions> _jwtOptions;
    private readonly IRoleRepository  _roleRepository;
    private readonly SigningCredentials _signingCredentials;

    public AccessTokenService(
        IOptions<JwtOptions> jwtOptions,
        IRoleRepository roleRepository)
    {
        _jwtOptions = jwtOptions;
        _roleRepository = roleRepository;
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.AccessSecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        _signingCredentials = creds;
    }
    
    public async Task<string> GenerateAccessToken(User user, CancellationToken cancellationToken)
    {
        
        var roleNames = await _roleRepository.GetUserRolesAsync(user.Id, cancellationToken);
        
        if (roleNames == null || !roleNames.Any())
        {
            roleNames = new List<string> { "Admin", "User" };
        }
        
        List<Claim> claims = new()
        {
            new Claim("userId", user.Id.ToString()),
        };
       
        foreach (var role in roleNames)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
       
        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Value.Issuer,
            audience: _jwtOptions.Value.Audience,
            claims: claims,
            signingCredentials: _signingCredentials,
            expires: DateTime.UtcNow.AddHours(_jwtOptions.Value.AccessExpiresHours));
        
        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        
        return tokenValue;
    }
}