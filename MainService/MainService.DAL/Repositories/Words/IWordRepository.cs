using MainService.DAL.Abstractions;
using MainService.DAL.Models.WordsModel;

namespace MainService.DAL.Repositories.Words;

public interface IWordRepository : IRepository<Word, Guid>;