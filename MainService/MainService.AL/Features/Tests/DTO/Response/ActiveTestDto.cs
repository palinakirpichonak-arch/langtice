using MainService.AL.Features.Tests.DTO.Request;

namespace MainService.AL.Features.Tests.DTO.Response;

public class ActiveTestDto 
{
    public string Title { get; set; } = null!;
    public List<QuestionDto> Questions { get; set; } = new();
}