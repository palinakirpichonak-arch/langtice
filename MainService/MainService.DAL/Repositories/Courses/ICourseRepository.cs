using MainService.DAL.Abstractions;
using MainService.DAL.Models.CoursesModel;

namespace MainService.DAL.Repositories.Courses;

public interface ICourseRepository  : IRepository<Course, Guid>;