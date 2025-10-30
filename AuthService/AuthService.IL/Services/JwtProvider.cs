using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthService.DAL.Features.Roles.Repositories;
using AuthService.DAL.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.IL.Services;

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _jwtOptions;
    private readonly IRoleRepository  _roleRepository;
    
    public JwtProvider(
        IOptions<JwtOptions> options,
        IRoleRepository roleRepository)
    {
        _jwtOptions = options.Value;
        _roleRepository = roleRepository;
    }
    
    public async Task<string> GenerateJwtToken(User user, CancellationToken cancellationToken)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var roleNames = await _roleRepository.GetUserRolesAsync(user.Id, cancellationToken);
        
       List<Claim> claims = new()
       {
           new(ClaimTypes.NameIdentifier, user.Id.ToString())
       };
       
       claims.AddRange(roleNames.Select(r => new Claim(ClaimTypes.Role, r)));
       
        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddHours(_jwtOptions.ExpiresHours));
        
        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        
        return tokenValue;
    }
}