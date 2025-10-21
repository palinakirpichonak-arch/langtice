using MainService.DAL.Abstractions;
using MainService.DAL.Features.Courses;

namespace MainService.DAL.Data.Courses;

public interface ICourseRepository  : IRepository<Course, Guid>;