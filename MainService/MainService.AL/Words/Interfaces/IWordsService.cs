using MainService.DAL.Models;
namespace MainService.AL.Words.Interfaces;

public interface IWordService
{
    Task<Word> GetWordByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Word>> GetAllWordsAsync(CancellationToken cancellationToken);
    Task<Word> AddWordAsync(Word word, CancellationToken cancellationToken);
    Task DeleteWordAsync(Word word, CancellationToken cancellationToken);
}