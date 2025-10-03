using MainService.AL.Features.Words.DTO;

public interface IUserWordService
{
    Task<IEnumerable<UserWord>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<UserWord?> GetByIdsAsync(Guid userId, Guid wordId, CancellationToken cancellationToken);
    Task<UserWord> CreateAsync(UserWordDTO dto, CancellationToken cancellationToken);
    Task DeleteAsync(UserWordKey id, CancellationToken cancellationToken);
}