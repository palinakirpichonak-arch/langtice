using MainService.DAL.Abstractions;
using MainService.DAL.Context.PostgreSql;
using MainService.DAL.Features.Lessons;

namespace MainService.DAL.Data.Lessons;

public class LessonRepository : Repository<Lesson, Guid>, ILessonRepository
{
    public LessonRepository(PostgreDbContext dbContext) : base(dbContext)
    {
    }
}