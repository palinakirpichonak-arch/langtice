namespace MainService.AL.Features.Words.DTO.Request;

public class RequestWordDto
{
    public Guid Id { get; set; }
    public string Text { get; set; } = null!;
}