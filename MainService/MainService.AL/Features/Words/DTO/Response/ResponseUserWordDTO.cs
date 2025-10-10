using MainService.DAL.Abstractions;

namespace MainService.AL.Features.Words.DTO.Response;

public class ResponseUserWordDto 
{
    public Guid UserId { get; set; }
    public PaginatedList<UserWordDto> UserWords { get; set; } = null!;
}

public class UserWordDto
{
    public string Word { get; set; } = null!;
    public DateTime? AddedAt { get; set; }
}
