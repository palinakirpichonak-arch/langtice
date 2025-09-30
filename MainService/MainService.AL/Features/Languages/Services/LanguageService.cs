using MainService.AL.Features.Languages.DTO;
using MainService.BLL.Data.Languages;
using MainService.DAL.Abstractions;
using MainService.DAL.Features.Languages.Models;
using MainService.IL.Services;

namespace MainService.AL.Features.Languages.Services;

public class LanguageService : Service<Language, LanguageDto, Guid>, ILanguageService
{
    public LanguageService(ILanguageRepository repository) : base(repository)
    {
    }
}