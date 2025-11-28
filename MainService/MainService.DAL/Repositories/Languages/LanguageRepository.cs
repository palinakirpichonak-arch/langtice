using MainService.DAL.Abstractions;
using MainService.DAL.Context.PostgreSql;
using MainService.DAL.Models.LanguagesModel;
using MainService.DAL.Repositories.Language_;

namespace MainService.DAL.Repositories.Languages;

public class LanguageRepository : Repository<Language, Guid>, ILanguageRepository
{
    public LanguageRepository(PostgreDbContext dbContext) : base(dbContext)
    {
    }
}