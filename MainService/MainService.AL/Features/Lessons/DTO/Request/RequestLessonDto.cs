using System.ComponentModel.DataAnnotations;

namespace MainService.AL.Features.Lessons.DTO.Request;

public class RequestLessonDto
{
    [Required (ErrorMessage = "CourseId is required")]
    public Guid CourseId { get; set; }
    
    [Required (ErrorMessage = "Lesson name is required")]
    [StringLength(120, MinimumLength = 10, ErrorMessage= "Lesson name must be between 10 and 120 characters")]
    public string Name { get; set; } = null!;
    
    public string? Description { get; set; }
    public int OrderNum { get; set; }
    public string? TestId { get; set; }
}