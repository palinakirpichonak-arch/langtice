using MainService.AL.Features.Abstractions;
using MainService.AL.Features.Words.DTO;
using MainService.DAL.Features.Words.Models;
using MainService.DAL.Models;

public interface IUserWordService : IService<UserWord,UserWordDTO, UserWordKey>
{
    Task<IEnumerable<UserWord>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task<UserWord?> GetByIdsAsync(Guid userId, Guid wordId, CancellationToken cancellationToken);
}