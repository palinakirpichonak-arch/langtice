namespace MainService.AL.Features.UserTests.DTO.Request;

public class RequestUserTestDto
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = null!;
    public int Count { get; set; } = 2;
    public string? Description { get; set; }
    public int OrderNum { get; set; }
}