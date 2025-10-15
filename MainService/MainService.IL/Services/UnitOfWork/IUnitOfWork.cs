using MainService.BLL.Data.Courses;
using MainService.BLL.Data.Languages;
using MainService.BLL.Data.Lessons;
using MainService.BLL.Data.Translations;
using MainService.BLL.Data.UserCourses;
using MainService.BLL.Data.UserTest;
using MainService.BLL.Data.UserWord;
using MainService.BLL.Data.Words;

namespace MainService.BLL.Services.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {   public ICourseRepository Courses { get; }
        public ILanguageRepository Languages { get; }
        public ILessonRepository Lessons { get; }
        public ITranslationRepository Translations { get; }
        public IWordRepository Words { get; }
        public IUserWordRepository UserWords { get; }
        public IUserCourseRepository UserCourses { get; }
        public IUserTestRepository UserTests { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}