using System;
using System.Collections.Generic;

namespace MainService.DAL.Models;

public  class Lesson
{
    public Guid Id { get; set; }

    public Guid CourseId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int OrderNum { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<LessonСontent> Lessoncontents { get; set; } = new List<LessonСontent>();
}
