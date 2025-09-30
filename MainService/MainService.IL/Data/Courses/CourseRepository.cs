using System.ComponentModel.DataAnnotations;
using MainService.DAL.Abstractions;
using MainService.DAL.Context;
using MainService.DAL.Features.Courses.Models;

namespace MainService.BLL.Data.Courses;

public class CourseRepository : Repository<Course, Guid>,ICourseRepository
{
    public CourseRepository(PostgreLangticeContext dbContext) : base(dbContext)
    {
    }
}