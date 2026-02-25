using System.ComponentModel.DataAnnotations;

namespace MainService.AL.Features.Translations.DTO.Request;

public class RequestTranslationDto 
{
    [Required(ErrorMessage = "FromWordId is required")]
    public Guid FromWordId { get; set; }
    [Required(ErrorMessage = "ToWordId is required")]
    public Guid ToWordId { get; set; }
    [Required(ErrorMessage = "CourseId is required")]
    public Guid? CourseId { get; set; }
}