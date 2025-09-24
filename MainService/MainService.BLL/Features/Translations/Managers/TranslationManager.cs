using MainService.DAL.Models;
using MainService.DAL.Words.Repository;


namespace MainService.BLL.Translations.Manager;

public class TranslationManager : Manager<Translation, Guid>, ITranslationManager
{
    private readonly ITranslationRepository _repository;

    public TranslationManager(ITranslationRepository repository) : base(repository)
    {
        _repository = repository;
    }
    
}