using MainService.DAL.Abstractions;
using MainService.DAL.Context;
using MainService.DAL.Features.Languages.Models;

namespace MainService.BLL.Data.Languages;

public class LanguageRepository : Repository<Language, Guid>, ILanguageRepository
{
    public LanguageRepository(PostgreDbContext dbContext) : base(dbContext)
    {
    }
}