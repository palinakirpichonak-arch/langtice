using MainService.DAL.Abstractions;

namespace MainService.BLL.Data.UserFlashCards;

public interface IUserFlashCardsRepository : IMongoRepository<DAL.Features.UserFlashCard.UserFlashCards, string>
{
    
}