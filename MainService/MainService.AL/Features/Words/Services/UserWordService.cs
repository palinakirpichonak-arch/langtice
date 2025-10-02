using MainService.AL.Features.Words.DTO;
using MainService.BLL.Data.Words.Repository;
using MainService.IL.Services;

namespace MainService.AL.Features.Words.Services;

public class UserWordService : Service<UserWord, UserWordDTO, UserWordKey>, IUserWordService
{
    private readonly IUserWordRepository _userWordRepository;

    public UserWordService(IUserWordRepository repository) : base(repository)
    {
        _userWordRepository = repository;
    }
    public async Task<IEnumerable<UserWord>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var allUserWords = await _repository.GetAllItemsByAsync(cancellationToken);
        return allUserWords.Where(uw => uw.UserId == userId);
    }

    public async Task<UserWord?> GetByIdsAsync(Guid userId, Guid wordId, CancellationToken cancellationToken)
    {
        return await _userWordRepository.GetByIdsAsync(userId, wordId, cancellationToken);
    }
}