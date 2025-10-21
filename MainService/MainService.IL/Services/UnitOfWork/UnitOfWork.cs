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