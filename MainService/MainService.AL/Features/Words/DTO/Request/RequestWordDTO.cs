namespace MainService.AL.Features.Words.DTO;

public class RequestWordDto
{
    public string Text { get; set; } = null!;
    public Guid LanguageId { get; set; }
}