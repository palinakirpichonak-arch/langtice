using System.ComponentModel.DataAnnotations;
using MainService.DAL.Models.TestModel;

namespace MainService.AL.Features.Tests.DTO.Request;

public class UserAnswerDto
{
    [Required(ErrorMessage = "Question answers are required")]
    public List<Question> Questions { get; set; } = new();
}