using MainService.BLL.Words.Interfaces;
using MainService.DAL.Models;
using MainService.DAL.Words.Repository;
namespace MainService.BLL.Words.Managers;

public class WordManager : IWordManager
{
    private readonly IWordRepository<Word> _wordRepository;

    public WordManager(IWordRepository<Word> wordRepository)
    {
        _wordRepository = wordRepository;
    }

    public async Task<Word> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _wordRepository.GetWordByIdAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<Word>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _wordRepository.GetAllWordsAsync(cancellationToken);
    }

    public async Task AddAsync(Word word, CancellationToken cancellationToken)
    {
        await _wordRepository.AddWordAsync(word, cancellationToken);
    }

    public async Task DeleteAsync(Word word, CancellationToken cancellationToken)
    {
        await _wordRepository.DeleteWordAsync(word, cancellationToken);
    }
}