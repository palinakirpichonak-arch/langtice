using MainService.AL.Features.Words.DTO.Response;
using MainService.DAL.Features.Words.Models;

namespace MainService.AL.Features.Translations.DTO.Response;

public class ResponseTranslationDto
{
    public Guid Id { get; set; }
    public ResponseWordDto FromWord { get; set; } 
    public ResponseWordDto ToWord { get; set; } 
    public Guid? CourseId { get; set; }
}