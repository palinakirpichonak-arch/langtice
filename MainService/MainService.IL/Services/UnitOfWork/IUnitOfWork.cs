namespace MainService.BLL.Services.UnitOfWork
{
    public interface IUnitOfWork 
    { 
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}