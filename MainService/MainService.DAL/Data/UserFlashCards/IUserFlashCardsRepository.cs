using MainService.DAL.Abstractions;

namespace MainService.DAL.Data.UserFlashCards
{
    public interface IUserFlashCardsRepository : IMongoRepository<Features.UserFlashCard.UserFlashCards, string>
    {
        Task<List<Features.UserFlashCard.UserFlashCards>> GetCardsForNotificationAsync(
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

