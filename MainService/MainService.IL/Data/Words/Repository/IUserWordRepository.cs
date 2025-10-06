using MainService.DAL.Abstractions;
using MainService.DAL.Features.Words.Models;

namespace MainService.BLL.Data.Words.Repository;

public interface IUserWordRepository: IRepository<UserWord, UserWordKey>
{
    
}