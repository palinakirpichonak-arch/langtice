using MainService.DAL.Models;

public interface ITranslationManager
{
    Task<Translation?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Translation>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Translation translation, CancellationToken cancellationToken);
    Task DeleteAsync(Translation translation, CancellationToken cancellationToken);
}