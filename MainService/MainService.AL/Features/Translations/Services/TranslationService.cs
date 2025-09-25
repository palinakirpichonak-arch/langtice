using MainService.AL.Features.Translations.Services;
using MainService.DAL;
using MainService.DAL.Abstractions;
using MainService.DAL.Features.Translations.Models;
using MainService.DAL.Models;
using MainService.IL.Services;

namespace MainService.IL.Translations.Services;

public class TranslationService : Service<Translation, Guid>, ITranslationService
{
    public TranslationService(IRepository<Translation, Guid> repository) : base(repository)
    {
    }
}