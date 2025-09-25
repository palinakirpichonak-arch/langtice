using MainService.DAL.Abstractions;
using MainService.DAL.Features.Users.Models;
using MainService.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace MainService.DAL.Features.Courses.Models;

public class UserCourse :  IEntity<UserCourseKey>
{
    public UserCourseKey Id
    {
        get => new();
        set
        {
            UserId = value.UserId;
            CourseId = value.CourseId;
        }
    }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public Guid CourseId { get; set; }
    public Course Course { get; set; } = null!;
}

[Owned]
public class UserCourseKey
{
    public UserCourseKey() { }
    public Guid UserId { get; set; }
    public Guid CourseId{get;set;}
}