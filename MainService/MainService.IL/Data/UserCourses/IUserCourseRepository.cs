using MainService.DAL.Abstractions;
using MainService.DAL.Features.UserCourse;

namespace MainService.BLL.Data.UserCourses;

public interface IUserCourseRepository : IRepository<UserCourse, UserCourseKey>;