using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace MainService.PL.Extensions;

public static class AuthenticationConfiguration
{
    public static IServiceCollection AddApiAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var issuer = configuration["Issuer"];
        var audience = configuration["Audience"];
        var secretKey = configuration["SecretKey"];
        
        if (string.IsNullOrWhiteSpace(issuer))
            throw new ArgumentException("JWT config error: Issuer is missing");

        if (string.IsNullOrWhiteSpace(audience))
            throw new ArgumentException("JWT config error: Audience is missing");

        if (string.IsNullOrWhiteSpace(secretKey) || secretKey.Length < 32)
            throw new ArgumentException("JWT config error: SecretKey is missing or too short (min 32 chars)");

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    RoleClaimType = ClaimTypes.Role,
                    NameClaimType = "userId",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["la-cookies"];
                        return Task.CompletedTask;
                    }
                };
            });

        services.AddAuthorization();

        return services;
    }
}
