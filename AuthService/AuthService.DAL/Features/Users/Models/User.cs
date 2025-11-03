namespace AuthService.DAL.Features.Users.Models;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string? AvatarUrl { get; set; }
    public bool Status { get; set; }
    public DateTime CreatedAt { get; set; }  
    
    public User() {}                        
    
    public static User Create(Guid id, string username, string email, string passwordHash, string? avatarUrl) =>
        new User
        {
            Id = id,
            Username = username,
            Email = email,
            PasswordHash = passwordHash,
            AvatarUrl = avatarUrl,
            Status = true,
            CreatedAt = DateTime.UtcNow
        };
}