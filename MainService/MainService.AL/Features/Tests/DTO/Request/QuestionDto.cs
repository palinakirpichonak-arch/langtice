namespace MainService.AL.Features.Tests.DTO.Request;

public class QuestionDto
{
    public int QuestionNumber { get; set; }
    public string Sentence { get; set; } = null!;
    public string[] AnswerOptions { get; set; } = null!;
}