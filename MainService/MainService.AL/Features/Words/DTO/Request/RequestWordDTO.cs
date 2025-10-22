using System.ComponentModel.DataAnnotations;

namespace MainService.AL.Features.Words.DTO.Request;

public class RequestWordDto
{
    [Required]
    [StringLength(20, MinimumLength = 2, ErrorMessage = "Word Name must be between 2 and 20 characters")]
    public string Text { get; set; } = null!;
    [Required(ErrorMessage = "Language Id is required")]
    public Guid LanguageId { get; set; }
}