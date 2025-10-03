using MainService.AL.Features.Lessons.DTO;
using MainService.DAL.Features.Courses.Models;

namespace MainService.AL.Features.Lessons.Services;

public interface ILessonService 
{
    Task<IEnumerable<Lesson>> GetAllByCourseIdAsync(Guid courseId, CancellationToken cancellationToken);
    Task<LessonDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<LessonDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<LessonDto> CreateAsync(LessonDto dto, CancellationToken cancellationToken);
    Task<LessonDto> UpdateAsync(Guid id, LessonDto dto, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}