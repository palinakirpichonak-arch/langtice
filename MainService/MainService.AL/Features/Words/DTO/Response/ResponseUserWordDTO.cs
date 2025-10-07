namespace MainService.AL.Features.Words.DTO.Response;

public class ResponseUserWordDto 
{
    public Guid UserId { get; set; }
    public ResponseWordDto Word { get; set; } = null!;
    public DateTime? AddedAt { get; set; }
}