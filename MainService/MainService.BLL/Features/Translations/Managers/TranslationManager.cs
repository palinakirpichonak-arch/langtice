using MainService.DAL.Models;
using MainService.DAL.Words.Repository;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MainService.DAL;

namespace MainService.BLL.Translations.Manager;

public class TranslationManager : ITranslationManager
{
    private readonly IRepository<Translation> _repository;

    public TranslationManager(IRepository<Translation> repository)
    {
        _repository = repository;
    }

    public async Task<Translation?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _repository.GetItemByIdAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<Translation>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _repository.GetAllItemsByAsync(cancellationToken);
    }

    public async Task AddAsync(Translation translation, CancellationToken cancellationToken)
    {
        await _repository.AddItemAsync(translation, cancellationToken);
    }

    public async Task DeleteAsync(Translation translation, CancellationToken cancellationToken)
    {
        await _repository.DeleteItemAsync(translation, cancellationToken);
    }
}