namespace MainService.AL.Features.Words.DTO.Response;

public class RequestTranslateWordDto
{
    public Guid? WordId { get; set; }
    public string? Text { get; set; }
    public Guid CourseId { get; set; }
}