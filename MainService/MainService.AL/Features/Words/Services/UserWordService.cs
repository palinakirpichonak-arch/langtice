using MainService.DAL;
using MainService.DAL.Abstractions;
using MainService.DAL.Features.Words.Models;
using MainService.DAL.Models;
using MainService.IL.Services;

namespace MainService.AL.Features.Words.Services;

public class UserWordService :  Service<UserWord, UserWordKey> ,IUserWordService
{
    public UserWordService(IRepository<UserWord, UserWordKey> repository) : base(repository)
    {
    }
}