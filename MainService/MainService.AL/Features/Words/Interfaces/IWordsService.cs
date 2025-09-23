using MainService.AL.Words.DTO;
using MainService.DAL.Models;
namespace MainService.AL.Words.Interfaces;

public interface IWordService
{
    Task<Word> GetWordByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Word>> GetAllWordsAsync(CancellationToken cancellationToken);
    Task<Word> AddWordAsync(WordDTO word, CancellationToken cancellationToken);
    Task DeleteWordAsync(Guid id, CancellationToken cancellationToken);
}