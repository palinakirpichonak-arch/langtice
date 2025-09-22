namespace MainService.DAL.Words.Repository;

public interface IWordRepository<T> where T : class
{
    public Task<T> GetWordByIdAsync(Guid id, CancellationToken cancellationToken);
    public Task<IEnumerable<T>> GetAllWordsAsync(CancellationToken cancellationToken);
    public Task AddWordAsync(T word, CancellationToken cancellationToken);
    public Task DeleteWordAsync(T word, CancellationToken cancellationToken);
}