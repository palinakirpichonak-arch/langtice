using MainService.DAL.Abstractions;

namespace AuthService.DAL.Users;

public class User : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string? AvatarUrl { get; set; }
    public bool? Status { get; set; }
}
