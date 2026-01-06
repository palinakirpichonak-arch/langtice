using MainService.DAL.Abstractions;
using MainService.DAL.Models.UserFlashCardModel;

namespace MainService.DAL.Repositories.UserFlashCards
{
    public interface IUserFlashCardsRepository : IMongoRepository<UserFlashCard, string>
    {
        Task<List<UserFlashCard>> GetCardsForNotificationAsync(
            TimeSpan lifetime,
            TimeSpan interval,
            DateTime now,
            CancellationToken ct);

        Task<bool> TryUpdateLastNotificationTimeAsync(
            string id,
            DateTime? previousValue,
            DateTime newValue,
            CancellationToken ct);
    }
}

