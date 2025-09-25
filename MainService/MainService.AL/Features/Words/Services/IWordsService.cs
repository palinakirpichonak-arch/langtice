using MainService.AL.Features.Abstractions;
using MainService.AL.Features.Words.DTO;
using MainService.DAL.Features.Words.Models;
using MainService.DAL.Models;
namespace MainService.AL.Words.Interfaces;

public interface IWordService : IService<Word,WordDto, Guid>
{
  
}