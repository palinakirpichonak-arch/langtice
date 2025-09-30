using MainService.DAL.Abstractions;
using MainService.DAL.Context;
using MainService.DAL.Features.Courses.Models;

namespace MainService.BLL.Data.Courses;

public class UserCourseRepository : Repository<UserCourse, UserCourseKey>, IUserCourseRepository
{
    public UserCourseRepository(PostgreLangticeContext dbContext) : base(dbContext)
    {
    }
}