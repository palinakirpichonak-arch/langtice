using MainService.DAL.Features.Words.Models;

namespace MainService.AL.Features.Translations.DTO.Response;

public class ResponseTranslationDto
{
    public Guid FromWordId { get; set; }
    public Word FromWord { get; set; } 
    public Guid ToWordId { get; set; }
    public Word ToWord { get; set; } 
    public Guid? CourseId { get; set; }
}