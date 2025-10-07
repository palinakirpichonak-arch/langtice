using MainService.DAL.Features.Courses.Models;

namespace MainService.AL.Features.Lessons.DTO.Request;

public class TestDto 
{
    public string Title { get; set; } = null!;
    public List<Question> Questions { get; set; } = new();
}