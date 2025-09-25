using MainService.DAL;
using MainService.DAL.Abstractions;
using MainService.DAL.Features.Translations.Models;
using MainService.DAL.Models;

namespace MainService.BLL.Data.Translations.Repository;

public interface ITranslationRepository : IRepository<Translation, Guid>
{
    public Task<Translation?> GetItemByWordCourseIdsAsync(Guid wordId,Guid courseId, CancellationToken cancellationToken);
}