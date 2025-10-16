using MainService.DAL.Abstractions;
using MainService.DAL.Context.MongoDb;

namespace MainService.BLL.Data.UserFlashCards;

public class UserFlashCardsRepository : MongoRepository<DAL.Features.UserFlashCard.UserFlashCards, string>, IUserFlashCardsRepository
{
    public UserFlashCardsRepository(MongoDbContext context) : base(context, "userflashcards")
    {
    }
}