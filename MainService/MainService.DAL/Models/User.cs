using System;
using System.Collections.Generic;

namespace MainService.DAL.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? AvatarUrl { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<Mistake> Mistakes { get; set; } = new List<Mistake>();

    public virtual ICollection<UserCourse> Usercourses { get; set; } = new List<UserCourse>();

    public virtual ICollection<UserInfo> Userinfos { get; set; } = new List<UserInfo>();

    public virtual ICollection<Word> Words { get; set; } = new List<Word>();
}
