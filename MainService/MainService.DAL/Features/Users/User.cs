using System.Text.Json.Serialization;
using MainService.DAL.Abstractions;

namespace MainService.DAL.Features.Users;

public class User : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string? AvatarUrl { get; set; }
    public bool? Status { get; set; }
    public UserInfo.UserInfo Userinfo { get; set; } 
    
    [JsonIgnore]
    public ICollection<UserCourse.UserCourse> UserCourses { get; set; } = new List<UserCourse.UserCourse>();
    
    [JsonIgnore]
    public ICollection<UserWord.UserWord> UserWords { get; set; } = new List<UserWord.UserWord>();
    [JsonIgnore]
    public ICollection<UserTest.UserTest> UserTests { get; set; } = new List<UserTest.UserTest>();

}
