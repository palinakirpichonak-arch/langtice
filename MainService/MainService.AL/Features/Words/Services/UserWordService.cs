using MainService.AL.Features.Words.DTO;
using MainService.BLL.Data.Words.Repository;
using MainService.DAL;
using MainService.DAL.Abstractions;
using MainService.DAL.Features.Words.Models;
using MainService.DAL.Models;
using MainService.IL.Services;

namespace MainService.AL.Features.Words.Services;

public class UserWordService : Service<UserWord, UserWordDTO, UserWordKey>, IUserWordService
{
    public UserWordService(IUserWordRepository repository) : base(repository) { }
    public async Task<IEnumerable<UserWord>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var allUserWords = await _repository.GetAllItemsByAsync(cancellationToken);
        return allUserWords.Where(uw => uw.UserId == userId);
    }
}