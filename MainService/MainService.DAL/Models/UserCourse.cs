using System;
using System.Collections.Generic;

namespace MainService.DAL.Models;

public partial class UserCourse
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid CourseId { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
