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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PostgreDbContext _dbContext;
        
        public UnitOfWork(PostgreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}