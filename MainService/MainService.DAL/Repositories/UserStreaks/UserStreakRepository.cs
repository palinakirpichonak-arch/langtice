using System.Linq.Expressions;
using MainService.DAL.Abstractions;
using MainService.DAL.Context.PostgreSql;
using MainService.DAL.Models.UserStreakModel;
using Microsoft.EntityFrameworkCore;

namespace MainService.DAL.Repositories.UserStreaks;

public class UserStreakRepository : Repository<UserStreak, Guid>, IUserStreakRepository
{
    public UserStreakRepository(PostgreDbContext dbContext) 
        : base(dbContext)
    {
    }

    public async Task<UserStreak?> GetByUserIdAsync(
        Guid userId, 
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<UserStreak>()
            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
    }

    public async Task<UserStreak?> UpdateOnUserVisitAsync(
        Guid userId, 
        DateTime visitTimeUtc,
        CancellationToken cancellationToken = default)
    {
        var today = DateOnly.FromDateTime(visitTimeUtc);

        var set = _dbContext.Set<UserStreak>();

        var streak = await set
            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);

        if (streak == null)
        {
            streak = new UserStreak
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                CurrentStreakDays = 1,
                LastActiveDate = today,
                CreatedAt = visitTimeUtc,
                UpdatedAt = visitTimeUtc
            };

            await set.AddAsync(streak, cancellationToken);
        }
        else
        {
            if (streak.LastActiveDate == today)
            {
                streak.UpdatedAt = visitTimeUtc;
            }
            else if (streak.LastActiveDate.HasValue &&
                     streak.LastActiveDate.Value.AddDays(1) == today)
            {
                streak.CurrentStreakDays++;
                streak.LastActiveDate = today;
                streak.UpdatedAt = visitTimeUtc;
            }
            else
            {
                streak.CurrentStreakDays = 1;
                streak.LastActiveDate = today;
                streak.UpdatedAt = visitTimeUtc;
            }

            set.Update(streak);
        }

        return streak;
    }

    public async Task<List<UserStreak>> GetForMorningReminderAsync(
        DateOnly today,
        DateTime nowUtc,
        CancellationToken cancellationToken = default)
    {
        if (nowUtc.Kind != DateTimeKind.Utc)
            nowUtc = DateTime.SpecifyKind(nowUtc, DateTimeKind.Utc);

        var startOfTodayUtc = new DateTime(
            nowUtc.Year, nowUtc.Month, nowUtc.Day,
            0, 0, 0, DateTimeKind.Utc);

        return await _dbContext.Set<UserStreak>()
            .AsNoTracking()
            .Where(x =>
                x.LastActiveDate != today &&
                (x.LastMorningNotificationAt == null ||
                 x.LastMorningNotificationAt < startOfTodayUtc) &&  
                x.CurrentStreakDays > 0)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<UserStreak>> GetForEveningReminderAsync(
        DateOnly today,
        DateTime nowUtc,
        CancellationToken cancellationToken = default)
    {
        if (nowUtc.Kind != DateTimeKind.Utc)
            nowUtc = DateTime.SpecifyKind(nowUtc, DateTimeKind.Utc);

        var startOfTodayUtc = new DateTime(
            nowUtc.Year, nowUtc.Month, nowUtc.Day,
            0, 0, 0, DateTimeKind.Utc);

        return await _dbContext.Set<UserStreak>()
            .AsNoTracking()
            .Where(x =>
                x.LastActiveDate != today &&
                (x.LastEveningNotificationAt == null ||
                 x.LastEveningNotificationAt < startOfTodayUtc) &&
                x.CurrentStreakDays > 0)
            .ToListAsync(cancellationToken);
    }

    public async Task MarkMorningNotifiedAsync(
        Guid id, 
        DateTime notifiedAtUtc,
        CancellationToken cancellationToken = default)
    {
        var set = _dbContext.Set<UserStreak>();
        var streak = await set.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (streak == null)
            return;

        streak.LastMorningNotificationAt = notifiedAtUtc;
        streak.UpdatedAt = notifiedAtUtc;

        set.Update(streak);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task MarkEveningNotifiedAsync(
        Guid id, 
        DateTime notifiedAtUtc,
        CancellationToken cancellationToken = default)
    {
        var set = _dbContext.Set<UserStreak>();
        var streak = await set.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (streak == null)
            return;

        streak.LastEveningNotificationAt = notifiedAtUtc;
        streak.UpdatedAt = notifiedAtUtc;

        set.Update(streak);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}