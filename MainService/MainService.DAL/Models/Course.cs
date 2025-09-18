using System;
using System.Collections.Generic;

namespace MainService.DAL.Models;

public partial class Course
{
    public Guid Id { get; set; }

    public Guid LearningLanguageId { get; set; }

    public Guid BaseLanguageId { get; set; }

    public bool? IsActive { get; set; }

    public virtual Language BaseLanguage { get; set; } = null!;

    public virtual Language LearningLanguage { get; set; } = null!;

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public virtual ICollection<UserCourse> Usercourses { get; set; } = new List<UserCourse>();
}
