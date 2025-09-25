using MainService.AL.Features.Translations.DTO;
using MainService.AL.Features.Translations.Services;
using MainService.BLL.Data.Translations.Repository;
using MainService.DAL;
using MainService.DAL.Abstractions;
using MainService.DAL.Features.Translations.Models;
using MainService.DAL.Models;
using MainService.IL.Services;

namespace MainService.IL.Translations.Services;

public class TranslationService : Service<Translation, TranslationDto, Guid>, ITranslationService
{
    public TranslationService(ITranslationRepository repository) : base(repository)
    {
    }
}