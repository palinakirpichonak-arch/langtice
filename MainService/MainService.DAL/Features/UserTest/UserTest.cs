using MainService.DAL.Abstractions;
using MainService.DAL.Features.Users;

namespace MainService.DAL.Features.UserTest;

public class UserTest : IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? TestId { get; set; }
    public int OrderNum { get; set; }
}