using AuthService.AL.Features.Users.Dto;
using AuthService.AL.Features.Users.Services;

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
        RegisterUserDto userDto, 
        UserService userService,
        CancellationToken cancellationToken)
    {
        await userService.RegisterUser(userDto, cancellationToken);
    }

    private static async Task<IResult> Login(
        LoginUserDto userDto,
        UserService userService, 
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var token = await userService.Login(userDto, cancellationToken);
        
        httpContext.Response.Cookies.Append("la-cookies", token);
        
        return Results.Ok();
    }

}