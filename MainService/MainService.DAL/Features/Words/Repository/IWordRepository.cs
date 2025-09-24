using MainService.DAL.Models;

namespace MainService.DAL.Words.Repository;

public interface IWordRepository : IRepository<Word, Guid>
{
    
}