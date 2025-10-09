using MainService.DAL.Abstractions;
using MainService.DAL.Features.Lessons;

namespace MainService.BLL.Data.Users;

public interface IUserFlashCardsRepository : IMongoRepository<UserFlashCards, string>
{
    
}