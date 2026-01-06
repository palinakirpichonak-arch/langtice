using AuthService.IL.Options;

namespace AuthService.IL.Sessions;

public interface ISessionStore
{
    Task StoreAsync(string jti, RefreshTokenState record, TimeSpan ttl, CancellationToken ct);
    Task<RefreshTokenState?> GetAsync(string jti, CancellationToken ct);
    ValueTask MarkRotatedAsync(string jti, CancellationToken ct);
    Task RevokeFamilyAsync(Guid familyId, CancellationToken ct);
}