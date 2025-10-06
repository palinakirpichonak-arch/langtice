using MainService.DAL.Abstractions;
using MainService.DAL.Features.Courses.Models;

namespace MainService.BLL.Data.Courses;

public interface IUserCourseRepository : IRepository<UserCourse, UserCourseKey>;