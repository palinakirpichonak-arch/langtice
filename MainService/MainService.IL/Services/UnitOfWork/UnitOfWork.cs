using MainService.BLL.Data.Courses;
using MainService.BLL.Data.Languages;
using MainService.BLL.Data.Lessons;
using MainService.BLL.Data.Translations;
using MainService.BLL.Data.UserCourses;
using MainService.BLL.Data.UserTest;
using MainService.BLL.Data.UserWord;
using MainService.BLL.Data.Words;
using MainService.DAL.Context.PostgreSql;

namespace MainService.BLL.Services.UnitOfWork
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
        public IUserTestRepository UserTests { get; }

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
            UserTests = new UserTestRepository(_dbContext);
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