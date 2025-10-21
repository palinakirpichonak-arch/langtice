using System.ComponentModel.DataAnnotations;

namespace MainService.AL.Features.Tests.DTO.Request;

public class QuestionDto
{
    public int QuestionNumber { get; set; }
    [Required(ErrorMessage = "Question sentence is required")]
    public string Sentence { get; set; } = null!;
    [Required(ErrorMessage = "Answer options are required")]
    public string[] AnswerOptions { get; set; } = null!;
}