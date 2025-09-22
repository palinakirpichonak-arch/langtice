using MainService.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace MainService.DAL.Words.Repository;

public class WordRepository : IWordRepository<Word>
{
    private readonly LangticeContext _dbContext;

    public WordRepository(LangticeContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Word> GetWordByIdAsync(Guid id,CancellationToken cancellationToken )
    {
        var word = await _dbContext.Words.FindAsync(id, cancellationToken);
        return word;
    }

    public async Task<IEnumerable<Word>> GetAllWordsAsync(CancellationToken cancellationToken)
    {
        var words = await _dbContext.Words.ToListAsync(cancellationToken);
        return words;
    }

    public async Task AddWordAsync(Word word, CancellationToken cancellationToken)
    {
        await _dbContext.Words.AddAsync(word, cancellationToken);
       await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteWordAsync(Word word,CancellationToken cancellationToken)
    {
        _dbContext.Words.Remove(word);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}