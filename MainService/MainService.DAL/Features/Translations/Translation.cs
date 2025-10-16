using MainService.DAL.Abstractions;
using MainService.DAL.Features.Courses;
using MainService.DAL.Features.Words;

namespace MainService.DAL.Features.Translations;

public class Translation : IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid FromWordId { get; set; }
    public Word FromWord { get; set; } = null!;
    public Guid ToWordId { get; set; }
    public Word ToWord { get; set; } = null!;
    public Guid? CourseId { get; set; }
    public Course? Course { get; set; } 
}
