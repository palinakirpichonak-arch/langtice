using MainService.DAL.Abstractions;
using MainService.DAL.Features.Words;

namespace MainService.BLL.Data.Words;

public interface IWordRepository : IRepository<Word, Guid>;