using MainService.DAL.Abstractions;
using MainService.DAL.Context;
using MainService.DAL.Features.Translations.Models;
using Microsoft.EntityFrameworkCore;

namespace MainService.BLL.Data.Translations.Repository;

public class TranslationRepository : Repository<Translation,Guid>, ITranslationRepository
{
    private readonly PostgreDbContext _dbContext;

    public TranslationRepository(PostgreDbContext dbContext) :  base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Translation?> GetItemByWordCourseIdsAsync(Guid wordId, Guid courseId, 
                                                        CancellationToken cancellationToken)
    {
        return await _dbContext.Set<Translation>()
                .Where(t => t.FromWordId == wordId && t.CourseId == courseId)
                .SingleAsync(cancellationToken);
    }

    public override async Task<IEnumerable<Translation>> GetAllItemsAsync(CancellationToken ct)
    {
        return await _dbContext.Translations
            .Include(t=>t.FromWord)
            .Include(t=>t.ToWord)
            .ToListAsync(ct);
    }

    public override async Task<Translation?> GetItemByIdAsync(Guid id, CancellationToken ct)
    {
        return await _dbContext.Translations
            .Include(t => t.FromWord)
            .Include(t => t.ToWord)
            .FirstOrDefaultAsync(t => t.Id == id, ct);
    }
}