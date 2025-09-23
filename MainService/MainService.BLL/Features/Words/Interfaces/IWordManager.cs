using MainService.DAL.Models;

namespace MainService.BLL.Words.Interfaces;

public interface IWordManager
{
    Task<Word> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Word>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Word word, CancellationToken cancellationToken);
    Task DeleteAsync(Word word, CancellationToken cancellationToken);
}