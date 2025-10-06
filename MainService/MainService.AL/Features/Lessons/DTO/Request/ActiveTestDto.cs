namespace MainService.AL.Features.Lessons.DTO.Request;

public class ActiveTestDto 
{
    public string Title { get; set; } = null!;
    public List<QuestionDto> Questions { get; set; } = new();
}