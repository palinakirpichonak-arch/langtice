using System.Text.Json.Serialization;
using MainService.DAL.Abstractions;
using MainService.DAL.Features.Courses.Models;
using MainService.DAL.Features.Words.Models;
using MainService.DAL.Models;

namespace MainService.DAL.Features.Users.Models;

public class User : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string? AvatarUrl { get; set; }
    public bool? Status { get; set; }
    public UserInfo Userinfo { get; set; } 
    
    [JsonIgnore]
    public ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();
    
    [JsonIgnore]
    public ICollection<UserWord> UserWords { get; set; } = new List<UserWord>();

}
