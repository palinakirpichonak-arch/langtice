using MainService.DAL.Abstractions;
using MainService.DAL.Context;
using MainService.DAL.Features.Courses.Models;

namespace MainService.BLL.Data.Lessons;

public class LessonRepository : Repository<Lesson, Guid>, ILessonRepository
{
    public LessonRepository(PostgreDbContext dbContext) : base(dbContext)
    {
    }
}