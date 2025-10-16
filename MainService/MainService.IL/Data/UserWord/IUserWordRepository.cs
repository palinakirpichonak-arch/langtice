using MainService.DAL.Abstractions;
using MainService.DAL.Features.UserWord;

namespace MainService.BLL.Data.UserWord;

public interface IUserWordRepository : IRepository<DAL.Features.UserWord.UserWord, UserWordKey>
{
    Task<PaginatedList<DAL.Features.UserWord.UserWord>> GetAllByUserIdAsync(Guid userId, int pageIndex, int pageSize, CancellationToken cancellationToken);
}