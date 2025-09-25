using MainService.DAL;
using MainService.DAL.Abstractions;
using MainService.DAL.Features.Words.Models;
using MainService.DAL.Models;

namespace MainService.BLL.Data.Words.Repository;

public interface IUserWordRepository: IRepository<UserWord, UserWordKey>
{
    
}