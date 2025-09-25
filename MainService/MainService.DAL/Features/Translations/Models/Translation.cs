using MainService.DAL.Abstractions;
using MainService.DAL.Features.Words.Models;
using MainService.DAL.Models;

namespace MainService.DAL.Features.Translations.Models;

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
