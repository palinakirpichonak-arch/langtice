namespace MainService.AL.Features.UserWords.DTO.Response;

public class ResponseUserWordDto 
{
    public Guid UserId { get; set; }
    public IEnumerable<UserWordDto> UserWords { get; set; } = null!;
}

public class UserWordDto
{
    public Guid Id { get; set; }
    public string Word { get; set; } = null!;
    public DateTime? AddedAt { get; set; }
}
