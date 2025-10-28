using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace MainService.PL.Extensions;

public static class AuthenticationConfiguration
{
    public static IServiceCollection AddApiAuthentication(this IServiceCollection services, IConfiguration jwtOptions)
    {
        var jwtSection = jwtOptions.GetSection("JwtOptions");
        var secretKey = jwtSection["SecretKey"];
        
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                };

                options.Events = new JwtBearerEvents()
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