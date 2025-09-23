using MainService.AL.Words.DTO;
using MainService.DAL.Models;

namespace MainService.BLL.Words.Service;

public class UserWordService : IUserWordService
{
    private readonly IUserWordManager _manager;

    public UserWordService(IUserWordManager manager)
    {
        _manager = manager;
    }

    public async Task<UserWord?> GetByIdsAsync(UserWordDTO userWordDto, CancellationToken cancellationToken)
    {
        return await _manager.GetByIdsAsync(userWordDto.UserId, userWordDto.UserId, cancellationToken);
    }

    public async Task<IEnumerable<UserWord>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _manager.GetAllByUserIdAsync(userId, cancellationToken);
    }

    public async Task AddUserWordAsync(UserWordDTO userWordDTO, CancellationToken cancellationToken)
    {
        UserWord userWord = new()
        {
            UserId = userWordDTO.UserId,
            WordId = userWordDTO.WordId,
            AddedAt = DateTime.Now
        };
        await _manager.AddAsync(userWord, cancellationToken);
    }

    public async Task DeleteUserWordAsync(UserWordDTO userWordDTO, CancellationToken cancellationToken)
    {
        var userWord = await _manager.GetByIdsAsync(userWordDTO.UserId, userWordDTO.WordId, cancellationToken);
        await _manager.DeleteAsync(userWord, cancellationToken);
    }
}