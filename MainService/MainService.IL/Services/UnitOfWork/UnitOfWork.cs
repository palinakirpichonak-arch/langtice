using MainService.BLL.Data.Courses;
using MainService.BLL.Data.Languages;
using MainService.BLL.Data.Translations.Repository;
using MainService.BLL.Data.Users;
using MainService.BLL.Data.Words.Repository;
using MainService.DAL.Context;

namespace MainService.BLL.Services
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly PostgreDbContext _dbContext;

        public ICourseRepository Courses { get; }
        public ILanguageRepository Languages { get; }
        public ILessonRepository Lessons { get; }
        public ITranslationRepository Translations { get; }
        public IWordRepository Words { get; }
        public IUserWordRepository UserWords { get; }
        public IUserCourseRepository UserCourses { get; }

        public UnitOfWork(PostgreDbContext dbContext)
        {
            _dbContext = dbContext;

            Courses = new CourseRepository(_dbContext);
            Languages = new LanguageRepository(_dbContext);
            Lessons = new LessonRepository(_dbContext);
            Translations = new TranslationRepository(_dbContext);
            Words = new WordRepository(_dbContext);
            UserWords = new UserWordRepository(_dbContext);
            UserCourses = new UserCourseRepository(_dbContext);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}