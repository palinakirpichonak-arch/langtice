using System;
using System.Collections.Generic;

namespace MainService.DAL.Models;

public partial class LessonСontent
{
    public Guid Id { get; set; }

    public Guid LessonId { get; set; }

    public string Type { get; set; } = null!;

    public int OrderNum { get; set; }

    public string MongoId { get; set; } = null!;

    public virtual Lesson Lesson { get; set; } = null!;

    public virtual ICollection<Mistake> Mistakes { get; set; } = new List<Mistake>();
}
