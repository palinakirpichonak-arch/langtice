using MainService.AL.Features.Lessons.DTO;
using MainService.AL.Features.Lessons.DTO.Request;
using MainService.AL.Features.Lessons.DTO.Response;
using MainService.DAL.Features.Courses.Models;

namespace MainService.AL.Features.Lessons.Services;

public interface ILessonService 
{
    Task<IEnumerable<Lesson>> GetAllByCourseIdAsync(Guid courseId, CancellationToken cancellationToken);
    Task<ResponseLessonDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<ResponseLessonDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<ResponseLessonDto> CreateAsync(RequestLessonDto dto, CancellationToken cancellationToken);
    Task<ResponseLessonDto> UpdateAsync(Guid id, RequestLessonDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}