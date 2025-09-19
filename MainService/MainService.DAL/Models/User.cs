using System;
using System.Collections.Generic;

namespace MainService.DAL.Models;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string? AvatarUrl { get; set; }
    public bool? Status { get; set; }
    public virtual UserInfo Userinfo { get; set; } 
    public virtual ICollection<UserCourse> UserCourses { get; set; } = new List<UserCourse>();
    public virtual ICollection<UserWord> UserWords { get; set; } = new List<UserWord>();
}
