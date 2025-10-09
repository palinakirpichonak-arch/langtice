using MainService.DAL.Abstractions;
using MainService.DAL.Context;
using MainService.DAL.Features.Lessons;

namespace MainService.BLL.Data.Courses;

public class LessonRepository : Repository<Lesson, Guid>, ILessonRepository
{
    public LessonRepository(PostgreDbContext dbContext) : base(dbContext)
    {
    }
}