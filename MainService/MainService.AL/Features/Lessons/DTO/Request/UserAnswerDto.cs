using MainService.DAL.Features.Lessons;

namespace MainService.AL.Features.Lessons.DTO.Request;

public class UserAnswerDto
{
    public List<Question> Questions { get; set; } = new();
}