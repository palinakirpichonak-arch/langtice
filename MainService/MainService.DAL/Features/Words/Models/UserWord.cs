using MainService.DAL.Abstractions;
using MainService.DAL.Features.Users.Models;
using MainService.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace MainService.DAL.Features.Words.Models;

public class UserWord : IEntity<UserWordKey>
{ 
    public UserWordKey Id
    {
        get => new UserWordKey();
        set
        {
            UserId = value.UserId;
            WordId = value.WordId;
        }
    }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public Guid WordId { get; set; }
    public Word Word { get; set; } = null!;
    public DateTime? AddedAt { get; set; }
}
[Owned]
public class UserWordKey
{
    public Guid UserId { get; set; }
    public Guid WordId { get; set; }

    public UserWordKey() {} 
}