using System;
using System.Collections.Generic;

namespace MainService.DAL.Models;

public class Translation
{
    public Guid Id { get; set; }
    public Guid FromWordId { get; set; }
    public Word FromWord { get; set; } = null!;
    public Guid ToWordId { get; set; }
    public Word ToWord { get; set; } = null!;
    public Guid? CourseId { get; set; }
    public Course? Course { get; set; } = null!;
}
