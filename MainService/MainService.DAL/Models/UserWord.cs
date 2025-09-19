namespace MainService.DAL.Models;

public class UserWord
{
    // public Guid Id { get; set; }   PRIMARY KEY(user_id, word_id)
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public Guid WordId { get; set; }
    public Word Word { get; set; } = null!;
    public DateTime? AddedAt { get; set; }
}