using MainService.DAL.Features.Test;

namespace MainService.AL.Features.Tests.DTO.Request;

public class UserAnswerDto
{
    public List<Question> Questions { get; set; } = new();
}