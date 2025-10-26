using MainService.DAL.Abstractions;

namespace MainService.DAL.Features.UserTest;

public class UserTest : IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? TestId { get; set; }
    public int OrderNum { get; set; }
}