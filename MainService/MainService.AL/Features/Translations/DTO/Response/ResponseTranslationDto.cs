using MainService.AL.Features.Words.DTO.Response;

namespace MainService.AL.Features.Translations.DTO.Response;

public class ResponseTranslationDto
{
    public Guid Id { get; set; }
    public ResponseWordDto FromWord { get; set; } 
    public ResponseWordDto ToWord { get; set; } 
    public Guid? CourseId { get; set; }
}