using MainService.DAL.Abstractions;
using MainService.DAL.Context.PostgreSql;
using MainService.DAL.Features.Courses;
using Microsoft.EntityFrameworkCore;

namespace MainService.DAL.Data.Courses;

public class CourseRepository : Repository<Course, Guid>,ICourseRepository
{
    public CourseRepository(PostgreDbContext dbContext) : base(dbContext)
    {
    }
}