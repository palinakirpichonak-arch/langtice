namespace MainService.AL.Features.UserFlashCards.Services;

public interface IUserFlashCardsService
{
    Task<IEnumerable<DAL.Features.UserFlashCard.UserFlashCards>> GetAllByUserAsync(Guid userId, CancellationToken cancellationToken);
    Task<DAL.Features.UserFlashCard.UserFlashCards?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<DAL.Features.UserFlashCard.UserFlashCards> GenerateFromUserWordsAsync(Guid userId, string? title, int count, CancellationToken cancellationToken);
    Task DeleteAsync(string id, CancellationToken cancellationToken);
}