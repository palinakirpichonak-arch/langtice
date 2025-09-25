using MainService.DAL.Abstractions;
using MainService.DAL.Models;

namespace MainService.DAL.Features.Courses.Models;

public class LessonСontent : IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid LessonId { get; set; }
    public Lesson Lesson { get; set; } = null!;
    public string Type { get; set; } = null!;
    public int OrderNum { get; set; }
    public string MongoId { get; set; } = null!;
}
