using System;
using System.Collections.Generic;

namespace MainService.DAL.Models;

public partial class Language
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Course> CourseBaseLanguages { get; set; } = new List<Course>();

    public virtual ICollection<Course> CourseLearningLanguages { get; set; } = new List<Course>();

    public virtual ICollection<Translation> Translations { get; set; } = new List<Translation>();

    public virtual ICollection<Word> Words { get; set; } = new List<Word>();
}
