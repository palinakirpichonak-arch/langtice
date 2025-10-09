using MainService.DAL.Features.Courses.Models;
using MainService.DAL.Features.Lessons;

namespace MainService.AL.Features.Lessons.DTO.Request;

public class UserTestDto
{
    public List<Question> Questions { get; set; } = new();
}