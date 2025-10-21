using System.ComponentModel.DataAnnotations;

namespace MainService.AL.Features.Words.DTO.Response;

public class RequestTranslateWordDto
{
    [Required(ErrorMessage = "WordId is required")]
    public Guid? WordId { get; set; }
    [Required(ErrorMessage = "Text is required")]
    [StringLength(25,MinimumLength = 2, ErrorMessage = "Text must be between 2 and 25 characters")]
    public string Text { get; set; } = null!;
    [Required(ErrorMessage = "CourseId is required")]
    public Guid CourseId { get; set; }
}