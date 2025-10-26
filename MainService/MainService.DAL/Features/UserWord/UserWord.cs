using System.ComponentModel.DataAnnotations.Schema;
using MainService.DAL.Abstractions;
using MainService.DAL.Features.Words;

namespace MainService.DAL.Features.UserWord;

public class UserWord : IEntity<UserWordKey>
{
    public Guid UserId { get; set; }

    public Guid WordId { get; set; }
    public Word Word { get; set; } = null!;
    public DateTime? AddedAt { get; set; }
    
    [NotMapped]
    public UserWordKey Id
    {
        get => new UserWordKey(UserId, WordId);
        set
        {
            UserId = value.UserId;
            WordId = value.WordId;
        }
    }
}

public class UserWordKey
{
    public Guid UserId { get; set; }
    public Guid WordId { get; set; }

    public UserWordKey(Guid userId, Guid wordId)
    {
        UserId = userId;
        WordId = wordId;
    }
}