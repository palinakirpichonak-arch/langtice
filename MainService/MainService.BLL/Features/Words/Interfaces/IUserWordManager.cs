using MainService.DAL.Models;

public interface IUserWordManager
{
    Task<UserWord?> GetByIdsAsync(Guid userId, Guid wordId, CancellationToken cancellationToken);
    Task<IEnumerable<UserWord>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task AddAsync(UserWord userWord, CancellationToken cancellationToken);
    Task DeleteAsync(UserWord userWord, CancellationToken cancellationToken);
}