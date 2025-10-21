using MainService.DAL.Abstractions;

namespace MainService.DAL.Data.UserFlashCards;

public interface IUserFlashCardsRepository : IMongoRepository<DAL.Features.UserFlashCard.UserFlashCards, string>
{
    
}