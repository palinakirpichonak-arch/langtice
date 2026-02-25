using MainService.DAL.Abstractions;
using MainService.DAL.Models.UserStreakModel;

namespace MainService.DAL.Repositories.UserStreaks;

public interface IUserStreakRepository : IRepository<UserStreak, Guid>
{
    Task<UserStreak?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<UserStreak?> UpdateOnUserVisitAsync(Guid userId, DateTime visitTimeUtc, CancellationToken cancellationToken = default);
    Task<List<UserStreak>> GetForMorningReminderAsync(DateOnly today, DateTime nowUtc, CancellationToken cancellationToken = default);
    Task<List<UserStreak>> GetForEveningReminderAsync(DateOnly today, DateTime nowUtc, CancellationToken cancellationToken = default);
    Task MarkMorningNotifiedAsync(Guid id, DateTime notifiedAtUtc, CancellationToken cancellationToken = default);
    Task MarkEveningNotifiedAsync(Guid id, DateTime notifiedAtUtc, CancellationToken cancellationToken = default);
}