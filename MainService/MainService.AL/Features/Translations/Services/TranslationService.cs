using MainService.AL.Translations.DTO;
using MainService.DAL.Models;
using MainService.AL.Translations.Interfaces;

namespace MainService.BLL.Translations.Service;

public class TranslationService 
{
    private readonly ITranslationManager _manager;

    public TranslationService(ITranslationManager manager)
    {
        _manager = manager;
    }
    
}