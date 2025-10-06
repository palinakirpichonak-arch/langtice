using MainService.AL.Features.Languages.DTO.Response;

namespace MainService.AL.Features.Courses.DTO.Response;

public class ResponseCourseDto
{
    public Guid Id { get; set; }
    public ResponseLanguageDto LearningLanguage { get; set; }
    public ResponseLanguageDto BaseLanguage { get; set; }
    public bool? Status { get; set; }
}