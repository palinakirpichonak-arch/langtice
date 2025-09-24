using MainService.DAL.Models;
namespace MainService.DAL.Words.Repository;

public interface ITranslationRepository : IRepository<Translation, Guid>
{
    public Task<Translation?> GetItemByWordCourseIdsAsync(Guid wordId,Guid courseId, CancellationToken cancellationToken);
}