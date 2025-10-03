using MainService.AL.Features.Languages.DTO;

namespace MainService.AL.Features.Courses.DTO.Response;

public class ResponseCourseDto
{
    public Guid Id { get; set; }
    public Guid LearningLanguageId { get; set; }
    public LanguageDto LearningLanguage { get; set; }
    public Guid BaseLanguageId { get; set; }
    public LanguageDto BaseLanguage { get; set; }
    public bool? Status { get; set; }
}