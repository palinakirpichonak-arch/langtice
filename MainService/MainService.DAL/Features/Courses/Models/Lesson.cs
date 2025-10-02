using MainService.DAL.Abstractions;
using MainService.DAL.Models;

namespace MainService.DAL.Features.Courses.Models;

public class Lesson : IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public Course Course { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? TestId { get; set; }
    public int OrderNum { get; set; }
}
