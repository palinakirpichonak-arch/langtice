using AuthService.AL.Features.Users.Dto;
using AuthService.AL.Features.Users.Services;
using AuthService.DAL.Users;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.PL.Users;

public  static class UserEndpoints
{
    public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("register", Register);
        app.MapPost("login", Login);

        return app;
    }
    private static async Task Register(
        [FromBody] RegisterUserDto userDto, 
        [FromServices] IUserService userService,
        CancellationToken cancellationToken)
    {
        await userService.RegisterUser(userDto, cancellationToken);
    }

    private static async Task Login(
        [FromBody] LoginUserDto userDto,
        [FromServices] IUserService userService, 
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var token = await userService.Login(userDto, cancellationToken);
        
        httpContext.Response.Cookies.Append("la-cookies", token, new CookieOptions()
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            Secure = true,
            Expires = DateTimeOffset.Now.AddHours(1)
        });
    }

}