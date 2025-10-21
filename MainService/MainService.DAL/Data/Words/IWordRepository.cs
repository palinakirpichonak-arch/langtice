using MainService.DAL.Abstractions;
using MainService.DAL.Features.Words;

namespace MainService.DAL.Data.Words;

public interface IWordRepository : IRepository<Word, Guid>;