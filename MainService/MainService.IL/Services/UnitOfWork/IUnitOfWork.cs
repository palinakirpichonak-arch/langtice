using MainService.BLL.Data.Courses;
using MainService.BLL.Data.Languages;
using MainService.BLL.Data.Translations.Repository;
using MainService.BLL.Data.Users;
using MainService.BLL.Data.Words.Repository;

namespace MainService.BLL.Services
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