using System;
using System.Collections.Generic;

namespace MainService.DAL.Models;

public class UserCourse
{
    public Guid UserId { get; set; }
    public virtual User User { get; set; } = null!;
    public Guid CourseId { get; set; }
    public virtual Course Course { get; set; } = null!;
}
