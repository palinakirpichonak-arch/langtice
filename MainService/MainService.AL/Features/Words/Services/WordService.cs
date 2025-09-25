using MainService.AL.Words.Interfaces;
using MainService.DAL;
using MainService.DAL.Abstractions;
using MainService.DAL.Features.Words.Models;
using MainService.DAL.Models;
using MainService.IL.Services;

namespace MainService.AL.Features.Words.Services
{
    public class WordService : Service<Word, Guid> , IWordService
    {
        public WordService(IRepository<Word, Guid> repository) : base(repository)
        {
        }
    }
}