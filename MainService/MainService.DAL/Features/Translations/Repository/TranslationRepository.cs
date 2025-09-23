using MainService.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace MainService.DAL.Words.Repository;

public class TranslationRepository : IRepository<Translation>
{
    private readonly LangticeContext _dbContext;

    public TranslationRepository(LangticeContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Translation?> GetItemByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Translations
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Translation>> GetAllItemsByAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Translations
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AddItemAsync(Translation item, CancellationToken cancellationToken)
    {
        if (item.Id == Guid.Empty) item.Id = Guid.NewGuid();
        await _dbContext.Translations.AddAsync(item, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteItemAsync(Translation item, CancellationToken cancellationToken)
    {
        _dbContext.Translations.Remove(item);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}