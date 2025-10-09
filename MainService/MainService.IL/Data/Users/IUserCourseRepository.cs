using MainService.DAL.Abstractions;
using MainService.DAL.Features.Courses.Models;

namespace MainService.BLL.Data.Users;

public interface IUserCourseRepository : IRepository<UserCourse, UserCourseKey>;