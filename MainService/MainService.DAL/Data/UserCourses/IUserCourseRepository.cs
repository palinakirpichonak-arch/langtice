using MainService.DAL.Abstractions;
using MainService.DAL.Features.UserCourse;

namespace MainService.DAL.Data.UserCourses;

public interface IUserCourseRepository : IRepository<UserCourse, UserCourseKey>;