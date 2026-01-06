using MainService.DAL.Abstractions;

namespace MainService.DAL.Models.UserStreakModel
{
    public class UserStreak : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int CurrentStreakDays { get; set; }
        public DateOnly? LastActiveDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? LastMorningNotificationAt { get; set; }
        public DateTime? LastEveningNotificationAt { get; set; }
    }
}

