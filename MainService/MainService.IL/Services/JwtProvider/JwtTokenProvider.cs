using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AuthService.PL.Services;

public class JwtTokenProvider : IJwtTokenProvider
{
    private readonly ILogger<JwtTokenProvider> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public JwtTokenProvider(ILogger<JwtTokenProvider> logger, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;

    }
    public string GetJwtToken()
    {
        _logger.LogInformation("Getting jwt token");
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext == null)
        {
            _logger.LogWarning("HttpContext is null in JwtTokenProvider");
            return null;
        }

        var token = httpContext.Request.Cookies["la-cookies"];
        _logger.LogInformation("la-cookies value: {token}", token ?? "<null>");

        return token;
    }
}