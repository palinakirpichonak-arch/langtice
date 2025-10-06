using MainService.DAL.Features.Courses.Models;

namespace MainService.AL.Features.Lessons.DTO.Request;

public class UserTestDto
{
    public List<Question> Questions { get; set; } = new();
}