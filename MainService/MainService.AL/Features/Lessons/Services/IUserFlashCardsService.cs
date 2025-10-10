using MainService.DAL.Abstractions;
using MainService.DAL.Features.Lessons;

namespace MainService.AL.Features.Lessons.Services;

public interface IUserFlashCardsService
{
    Task<IEnumerable<UserFlashCards>> GetAllByUserAsync(Guid userId, CancellationToken cancellationToken);
    Task<UserFlashCards?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<UserFlashCards> GenerateFromUserWordsAsync(Guid userId, string? title, int count, CancellationToken cancellationToken);
    Task DeleteAsync(string id, CancellationToken cancellationToken);
}