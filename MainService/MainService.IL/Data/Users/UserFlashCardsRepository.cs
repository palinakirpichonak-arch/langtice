using MainService.DAL.Abstractions;
using MainService.DAL.Context;
using MainService.DAL.Features.Lessons;

namespace MainService.BLL.Data.Users;

public class UserFlashCardsRepository : MongoRepository<UserFlashCards, string>, IUserFlashCardsRepository
{
    public UserFlashCardsRepository(MongoDbContext context) : base(context, "flashcards")
    {
    }
}