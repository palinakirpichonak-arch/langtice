using MainService.AL.Features.Abstractions;
using MainService.AL.Features.Languages.DTO;
using MainService.DAL.Features.Languages.Models;

namespace MainService.AL.Features.Languages.Services;

public interface ILanguageService: IService<Language, LanguageDto, Guid>
{
    
}