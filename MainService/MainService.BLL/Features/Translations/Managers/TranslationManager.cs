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
    private readonly ITranslationRepository _repository;

    public TranslationManager(ITranslationRepository repository)
    {
        _repository = repository;
    }

    public async Task<Translation?> GetByIdAsync(Guid wordId, CancellationToken cancellationToken)
    {
        return await _repository.GetItemByIdAsync(wordId, cancellationToken);
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
    public Task<IEnumerable<Translation>> GetForWordInCourseAsync(Guid fromWordId, Guid courseId, CancellationToken cancellationToken)
        => _repository.GetTranslationsForWordInCourseAsync(fromWordId, courseId, cancellationToken);

    public Task<IEnumerable<Translation>> GetForUserWordsAsync(IEnumerable<Guid> wordIds, Guid courseId, CancellationToken cancellationToken)
        => _repository.GetTranslationsForUserWordsAsync(wordIds, courseId, cancellationToken);
}