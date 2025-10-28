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

    private User(Guid id, string username, string email, string passwordHash, string? avatarUrl)
    {
        Id = id;
        Username = username;
        Email = email;
        PasswordHash = passwordHash;
        AvatarUrl = avatarUrl;
        Status = true;
    }

    public static User Create(Guid id, string username, string email, string passwordHash, string? avatarUrl) => 
        new User(id, username, email, passwordHash, avatarUrl);
    
}
