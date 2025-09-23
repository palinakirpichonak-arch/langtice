using MainService.BLL.Words.Interfaces;
using MainService.DAL;
using MainService.DAL.Models;
using MainService.DAL.Words.Repository;
namespace MainService.BLL.Words.Managers;

public class WordManager : IWordManager
{
    private readonly IRepository<Word> _wordRepository;

    public WordManager(IRepository<Word> wordRepository)
    {
        _wordRepository = wordRepository;
    }

    public async Task<Word> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _wordRepository.GetItemByIdAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<Word>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _wordRepository.GetAllItemsByAsync(cancellationToken);
    }

    public async Task AddAsync(Word word, CancellationToken cancellationToken)
    {
        await _wordRepository.AddItemAsync(word, cancellationToken);
    }

    public async Task DeleteAsync(Word word, CancellationToken cancellationToken)
    {
        await _wordRepository.DeleteItemAsync(word, cancellationToken);
    }
}