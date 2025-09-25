using MainService.DAL.Abstractions;
using MainService.DAL.Features.Words.Models;
using MainService.DAL.Models;

namespace MainService.DAL.Features.Languages.Models;

public class Language : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<Course> CoursesAsBase { get; set; } = new List<Course>();
    public ICollection<Course> CoursesAsLearning { get; set; } = new List<Course>();
    public ICollection<Word> Words { get; set; } = new List<Word>();
}