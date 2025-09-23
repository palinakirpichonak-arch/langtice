using MainService.AL.Words.DTO;
using MainService.DAL.Models;

public interface IUserWordService
{
    Task<UserWord?> GetByIdsAsync(UserWordDTO userWordDTO, CancellationToken cancellationToken);
    Task<IEnumerable<UserWord>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task AddUserWordAsync(UserWordDTO userWordDTO, CancellationToken cancellationToken);
    Task DeleteUserWordAsync(UserWordDTO userWordDTO, CancellationToken cancellationToken);
}