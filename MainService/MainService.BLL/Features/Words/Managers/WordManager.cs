using MainService.BLL.Words.Interfaces;
using MainService.DAL;
using MainService.DAL.Models;
using MainService.DAL.Words.Repository;
namespace MainService.BLL.Words.Managers;

public class WordManager : Manager<Word, Guid>, IWordManager
{
    public WordManager(IRepository<Word, Guid> repository) : base(repository)
    {
    }
}