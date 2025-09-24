using System;
using System.Collections.Generic;

namespace MainService.DAL.Models;

public class UserCourse :  IEntity<UserCourseKey>
{
    public UserCourseKey Id
    {
        get => new(UserId, CourseId);
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

public class UserCourseKey
{
    public UserCourseKey(Guid userId, Guid courseId)
    {
        UserId = userId;
        CourseId = courseId;
    }
    public Guid UserId;
    public Guid CourseId;
}