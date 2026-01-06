using MainService.DAL.Abstractions;
using MainService.DAL.Models.UserCourseModel;

namespace MainService.DAL.Repositories.UserCourses;

public interface IUserCourseRepository : IRepository<UserCourse, UserCourseKey>;