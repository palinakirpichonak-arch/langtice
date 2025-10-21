using MainService.DAL.Abstractions;
using MainService.DAL.Features.Lessons;

namespace MainService.DAL.Data.Lessons;

public interface ILessonRepository : IRepository<Lesson, Guid>;