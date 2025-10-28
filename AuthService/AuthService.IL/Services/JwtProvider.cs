using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthService.DAL.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.IL.Services;

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _jwtOptions;
    
    public JwtProvider(IOptions<JwtOptions> options)
    {
        _jwtOptions = options.Value;
    }
    
    public string GenerateJwtToken(User user)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        Claim[] claims = [new("userId", user.Id.ToString())];
        
        var token = new JwtSecurityToken(
            signingCredentials: signingCredentials,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(_jwtOptions.ExpiresHours));
        
        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        
        return tokenValue;
    }
}