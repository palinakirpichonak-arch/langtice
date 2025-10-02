using MainService.DAL.Abstractions;
using MainService.DAL.Features.Courses.Models;
using MainService.DAL.Models;

namespace MainService.BLL.Data.Lessons;

public interface ILessonRepository : IRepository<Lesson, Guid>
{
    
}