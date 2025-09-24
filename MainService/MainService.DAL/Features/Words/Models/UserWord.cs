namespace MainService.DAL.Models;

public class UserWord : IEntity<UserWordKey>
{ 
    public UserWordKey Id
    {
        get => new UserWordKey(UserId, WordId);
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
public class UserWordKey
{
    public UserWordKey(Guid userId, Guid wordId)
    {
        UserId = userId;
        WordId = wordId;
    }
    public Guid UserId;
    public Guid WordId;
}