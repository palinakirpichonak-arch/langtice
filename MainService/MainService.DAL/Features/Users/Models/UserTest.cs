using MainService.DAL.Abstractions;

namespace MainService.DAL.Features.Users.Models;

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