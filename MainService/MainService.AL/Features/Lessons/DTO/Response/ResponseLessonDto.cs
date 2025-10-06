namespace MainService.AL.Features.Lessons.DTO;

public class ResponseLessonDto
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int OrderNum { get; set; }
    public string? TestId { get; set; }
}