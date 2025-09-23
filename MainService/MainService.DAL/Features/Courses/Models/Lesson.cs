namespace MainService.DAL.Models;

public class Lesson
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public Course Course { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int OrderNum { get; set; }
    public ICollection<LessonСontent> LessonContents { get; set; } = new List<LessonСontent>();
}
