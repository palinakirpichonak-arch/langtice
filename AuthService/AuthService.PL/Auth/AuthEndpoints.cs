using AuthService.AL.Features;
using AuthService.AL.Features.Users.Dto;
using AuthService.AL.Features.Users.Services;
using AuthService.IL.Options;
using Microsoft.Extensions.Options;

namespace AuthService.PL.Auth;

public static class AuthEndpoints
{
    public record LoginRequest(string Email, string Password);
    public record RefreshRequest(string RefreshToken);

    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/auth");

        group.MapPost("/register", async (
            RegisterUserDto dto,
            IUserService userService,
            CancellationToken ct) =>
        {
            await userService.RegisterUser(dto, ct);
            return Results.Ok();
        });

        group.MapPost("/login", async (
            LoginRequest body,
            IAppAuthService auth,
            IOptions<JwtOptions> jwtOpt,
            HttpContext http,
            CancellationToken ct) =>
        {
            var (access, refresh) = await auth.LoginAsync(
                new LoginUserDto { Email = body.Email, Password = body.Password },
                ct);

            http.Response.Cookies.Append("la-cookies", access, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Path = "/",
                Expires = DateTimeOffset.UtcNow.AddHours(jwtOpt.Value.AccessExpiresHours)
            });

            http.Response.Cookies.Append("la-fresh-cookie", refresh, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Path = "/auth",
                Expires = DateTimeOffset.UtcNow.AddDays(jwtOpt.Value.RefreshExpiresDays)
            });

            return Results.Ok(new { access_token = access, refresh_token = refresh });
        });

        group.MapPost("/refresh", async (
            RefreshRequest body,
            IAppAuthService auth,
            IOptions<JwtOptions> jwtOpt,
            HttpContext http,
            CancellationToken ct) =>
        {
            var incomingRefresh = body.RefreshToken ?? http.Request.Cookies["la-fresh-cookie"];
            if (string.IsNullOrEmpty(incomingRefresh))
                return Results.Unauthorized();

            var (newAccess, newRefresh) = await auth.RefreshAsync(incomingRefresh, ct);
            
            http.Response.Cookies.Append("la-cookies", newAccess, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Path = "/",
                Expires = DateTimeOffset.UtcNow.AddHours(jwtOpt.Value.AccessExpiresHours)
            });
            
            http.Response.Cookies.Append("la-fresh-cookie", newRefresh, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Path = "/auth",
                Expires = DateTimeOffset.UtcNow.AddDays(jwtOpt.Value.RefreshExpiresDays)
            });

            return Results.Ok(new { access_token = newAccess, refresh_token = newRefresh });
        });

        group.MapPost("/logout", async (
            RefreshRequest body,
            IAppAuthService auth,
            HttpContext http,
            CancellationToken ct) =>
        {
            var incomingRefresh = body.RefreshToken ?? http.Request.Cookies["la-fresh-cookie"];
            if (!string.IsNullOrEmpty(incomingRefresh))
            {
                await auth.LogoutAsync(incomingRefresh, ct);
            }

            http.Response.Cookies.Delete("la-cookies", new CookieOptions { Path = "/" });
            http.Response.Cookies.Delete("la-fresh-cookie", new CookieOptions { Path = "/auth" });

            return Results.NoContent();
        });

        return app;
    }
}