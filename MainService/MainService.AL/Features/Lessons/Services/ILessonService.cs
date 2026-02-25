using MainService.AL.Features.Lessons.DTO.Request;
using MainService.AL.Features.Lessons.DTO.Response;

namespace MainService.AL.Features.Lessons.Services;

public interface ILessonService 
{
    Task<ResponseLessonDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<ResponseLessonDto>> GetAllWithCourseIdAsync(Guid courseId, int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<ResponseLessonDto> CreateAsync(RequestLessonDto dto, CancellationToken cancellationToken);
    Task<ResponseLessonDto> UpdateAsync(Guid id, RequestLessonDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}