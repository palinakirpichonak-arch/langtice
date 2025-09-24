using MainService.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace MainService.DAL.Words.Repository;

public class WordRepository : IRepository<Word>
{
    private readonly LangticeContext _dbContext;

    public WordRepository(LangticeContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Word?> GetItemByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var word = await _dbContext.Words.FindAsync(id, cancellationToken);
        return word;
    }

    public async Task<IEnumerable<Word>> GetAllItemsByAsync(CancellationToken cancellationToken)
    {
        var words = await _dbContext.Words.ToListAsync(cancellationToken);
        return words;
    }

    public async Task AddItemAsync(Word item, CancellationToken cancellationToken)
    {
        await _dbContext.Words.AddAsync(item, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateItemAsync(Word item, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteItemAsync(Word item, CancellationToken cancellationToken)
    {
        _dbContext.Words.Remove(item);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}