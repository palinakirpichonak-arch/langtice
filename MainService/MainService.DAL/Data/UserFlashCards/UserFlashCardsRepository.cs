using MainService.DAL.Abstractions;
using MainService.DAL.Constants;
using MainService.DAL.Context.MongoDb;

namespace MainService.DAL.Data.UserFlashCards;

public class UserFlashCardsRepository : MongoRepository<DAL.Features.UserFlashCard.UserFlashCards, string>, IUserFlashCardsRepository
{
    public UserFlashCardsRepository(MongoDbContext context) : base(context, MongoDbCollections.FlashCardsCollectionName)
    {
    }
}