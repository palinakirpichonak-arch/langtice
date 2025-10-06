using MainService.DAL.Features.Words.Models;

namespace MainService.AL.Features.Translations.DTO.Response;

public class ResponseTranslationDto
{
    public Guid Id { get; set; }
    public Word FromWord { get; set; } 
    public Word ToWord { get; set; } 
    public Guid? CourseId { get; set; }
}