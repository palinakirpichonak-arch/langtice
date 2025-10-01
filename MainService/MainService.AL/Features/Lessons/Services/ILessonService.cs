using MainService.AL.Features.Abstractions;
using MainService.AL.Features.Lessons.DTO;
using MainService.DAL.Features.Courses.Models;
using MainService.DAL.Models;

namespace MainService.AL.Features.Lessons.Services;

public interface ILessonService : IService<Lesson, LessonDto, Guid>
{
    Task<IEnumerable<Lesson>> GetAllByCourseIdAsync(Guid courseId, CancellationToken cancellationToken);
}