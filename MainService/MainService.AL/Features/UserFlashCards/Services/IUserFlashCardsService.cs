using MainService.AL.Features.UserFlashCards.DTO.Request;

namespace MainService.AL.Features.UserFlashCards.Services;

public interface IUserFlashCardsService
{
    Task<IEnumerable<DAL.Models.UserFlashCardModel.UserFlashCard>> GetAllByUserAsync(Guid userId, CancellationToken cancellationToken);
    Task<DAL.Models.UserFlashCardModel.UserFlashCard?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<DAL.Models.UserFlashCardModel.UserFlashCard> GenerateFromUserWordsAsync(RequestUserFlashCardDto dto, Guid userId, CancellationToken cancellationToken);
    Task DeleteAsync(string id, CancellationToken cancellationToken);
}