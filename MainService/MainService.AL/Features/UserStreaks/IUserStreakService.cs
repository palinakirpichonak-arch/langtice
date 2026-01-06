using MainService.DAL.Models.UserStreakModel;

namespace MainService.AL.Features.UserStreaks
{
    public interface IUserStreakService
    {
        Task TrackUserVisitAsync(Guid userId, DateTime visitTimeUtc, CancellationToken cancellationToken = default);
    }
}

