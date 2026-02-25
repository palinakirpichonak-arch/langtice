namespace MainService.AL.Features.Words.DTO.Response;
public class ResponseTranslateWordDto
{
    public Guid FromWordId { get; set; }
    public string FromText { get; set; } = string.Empty;
    public Guid ToWordId { get; set; }
    public string ToText { get; set; } = string.Empty;
    public Guid CourseId { get; set; }
}
