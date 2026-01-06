using MainService.DAL.Abstractions;
using MainService.DAL.Models.UserWordModel;

namespace MainService.DAL.Repositories.UserWords;

public interface IUserWordRepository : IRepository<UserWord, UserWordKey>
{
    Task<PaginatedList<UserWord>> GetAllByUserIdAsync(Guid userId, int pageIndex, int pageSize, CancellationToken cancellationToken);
}