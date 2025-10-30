using System.Security.Claims;

namespace MainService.PL.Extensions;

public static class ClaimsExtension
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var id = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        return string.IsNullOrEmpty(id) ? Guid.Empty : Guid.Parse(id);
    }
}