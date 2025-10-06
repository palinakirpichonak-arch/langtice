namespace MainService.AL.Features.Translations.DTO;

public class RequestTranslationDto 
{
    public Guid FromWordId { get; set; }
    public Guid ToWordId { get; set; }
    public Guid? CourseId { get; set; }
}