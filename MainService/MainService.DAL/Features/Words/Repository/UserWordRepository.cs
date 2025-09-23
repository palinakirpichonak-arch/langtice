using MainService.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace MainService.DAL.Words.Repository;

public class UserWordRepository : IRepository<UserWord>, IUserWordRepository<UserWord>
{
    private readonly LangticeContext _dbContext;

    public UserWordRepository(LangticeContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserWord?> GetByIdsAsync(Guid userId, Guid wordId, CancellationToken cancellationToken)
    {
        return await _dbContext.UserWords
            .FirstOrDefaultAsync(uw => uw.UserId == userId && uw.WordId == wordId, cancellationToken);
    }

    public async Task<IEnumerable<UserWord>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _dbContext.UserWords
            .Where(uw => uw.UserId == userId)
            .Include(uw => uw.Word)
            .ToListAsync(cancellationToken);
    }

    public async Task AddItemAsync(UserWord item, CancellationToken cancellationToken)
    {
        await _dbContext.UserWords.AddAsync(item, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteItemAsync(UserWord item, CancellationToken cancellationToken)
    {
        _dbContext.UserWords.Remove(item);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
    
    public Task<UserWord?> GetItemByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserWord>> GetAllItemsByAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}