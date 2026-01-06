using MainService.DAL.Abstractions;
using MainService.DAL.Context.PostgreSql;
using MainService.DAL.Models.CoursesModel;

namespace MainService.DAL.Repositories.Courses;

public class CourseRepository : Repository<Course, Guid>,ICourseRepository
{
    public CourseRepository(PostgreDbContext dbContext) : base(dbContext)
    {
    }
}