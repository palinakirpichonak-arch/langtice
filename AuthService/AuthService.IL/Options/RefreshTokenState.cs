namespace AuthService.IL.Options;

public class RefreshTokenState
{
    public Guid UserId { get; init; }
    public Guid FamilyId { get; init; }
    public DateTime ExpiresAtUtc { get; init; }
    public bool Rotated { get; set; }
}