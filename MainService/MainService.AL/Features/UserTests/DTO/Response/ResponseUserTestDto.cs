namespace MainService.AL.Features.UserTests.DTO.Response;

public class ResponseUserTestDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int OrderNum { get; set; }
    public string? TestId { get; set; }
}