using MainService.DAL.Abstractions;

namespace MainService.BLL.Data.Words.Repository;

public interface IUserWordRepository: IRepository<UserWord, UserWordKey>
{
    public Task<UserWord?> GetByIdsAsync(Guid userId, Guid wordId, CancellationToken cancellationToken);
}