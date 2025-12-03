using MainService.BLL.Services.UnitOfWork;
using MainService.DAL.Abstractions;
using MainService.DAL.Models.UserStreakModel;
using MainService.DAL.Repositories.UserStreaks;
using Microsoft.Extensions.Logging;

namespace MainService.AL.Features.UserStreaks
{
    public class UserStreakService : IUserStreakService
    {
        private readonly ILogger<UserStreakService> _logger;
        private readonly IUserStreakRepository _streakRepository;
        private IUnitOfWork _unitOfWork;

        public UserStreakService(
            ILogger<UserStreakService> logger,
            IUserStreakRepository streakRepository,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _streakRepository = streakRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task TrackUserVisitAsync(
            Guid userId,
            DateTime visitTimeUtc,
            CancellationToken cancellationToken = default)
        {
            await _streakRepository.UpdateOnUserVisitAsync(userId, visitTimeUtc, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("User {UserId} streak updated on visit at {VisitTimeUtc}", userId, visitTimeUtc);
        }
    }
}

