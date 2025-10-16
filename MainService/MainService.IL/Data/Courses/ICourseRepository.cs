using MainService.DAL.Abstractions;
using MainService.DAL.Features.Courses;

namespace MainService.BLL.Data.Courses;

public interface ICourseRepository  : IRepository<Course, Guid>;