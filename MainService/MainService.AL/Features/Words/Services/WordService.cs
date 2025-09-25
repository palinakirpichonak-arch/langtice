using MainService.AL.Features.Words.DTO;
using MainService.AL.Words.Interfaces;
using MainService.BLL.Data.Words.Repository;
using MainService.DAL;
using MainService.DAL.Abstractions;
using MainService.DAL.Features.Words.Models;
using MainService.DAL.Models;
using MainService.IL.Services;

namespace MainService.AL.Features.Words.Services
{
    public class WordService : Service<Word,WordDto, Guid> , IWordService
    {
        public WordService(IWordRepository repository) : base(repository)
        {
        }
    }
}