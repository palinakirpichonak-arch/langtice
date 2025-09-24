using MainService.DAL.Models;
using MainService.DAL.Words.Repository;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MainService.BLL.Words.Interfaces;
using MainService.DAL;

namespace MainService.BLL.Words.Manager;

public class UserWordManager : Manager<UserWord, UserWordKey>,IUserWordManager
{
    public UserWordManager(IRepository<UserWord, UserWordKey> repository) : base(repository)
    {
    }
}