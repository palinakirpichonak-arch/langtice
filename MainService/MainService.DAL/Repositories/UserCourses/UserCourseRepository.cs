using MainService.DAL.Abstractions;
using MainService.DAL.Context.PostgreSql;
using MainService.DAL.Models.UserCourseModel;
using Microsoft.EntityFrameworkCore;

namespace MainService.DAL.Repositories.UserCourses;

public class UserCourseRepository : Repository<UserCourse, UserCourseKey>, IUserCourseRepository
{
    public UserCourseRepository(PostgreDbContext dbContext) : base(dbContext) { }
}