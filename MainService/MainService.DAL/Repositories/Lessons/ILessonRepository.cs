using MainService.DAL.Abstractions;
using MainService.DAL.Models.LessonsModel;

namespace MainService.DAL.Repositories.Lessons;

public interface ILessonRepository : IRepository<Lesson, Guid>;