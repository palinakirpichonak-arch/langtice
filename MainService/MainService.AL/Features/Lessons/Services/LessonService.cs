using MainService.AL.Features.Lessons.DTO;
using MainService.BLL.Data.Lessons;
using MainService.DAL.Abstractions;
using MainService.DAL.Features.Courses.Models;
using MainService.DAL.Models;
using MainService.IL.Services;

namespace MainService.AL.Features.Lessons.Services;

public class LessonService : Service<Lesson, LessonDto, Guid>, ILessonService
{
    public LessonService(ILessonRepository repository) : base(repository)
    {
        
    }

    public async Task<IEnumerable<Lesson>> GetAllByCourseIdAsync(Guid courseId, CancellationToken cancellationToken)
    {
        var allLessons = await _repository.GetAllItemsByAsync(cancellationToken);
        return allLessons.Where(l => l.CourseId == courseId);
    }
}