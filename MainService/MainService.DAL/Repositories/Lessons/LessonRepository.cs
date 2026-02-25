using MainService.DAL.Abstractions;
using MainService.DAL.Context.PostgreSql;
using MainService.DAL.Models.LessonsModel;

namespace MainService.DAL.Repositories.Lessons;

public class LessonRepository : Repository<Lesson, Guid>, ILessonRepository
{
    public LessonRepository(PostgreDbContext dbContext) : base(dbContext)
    {
    }
}