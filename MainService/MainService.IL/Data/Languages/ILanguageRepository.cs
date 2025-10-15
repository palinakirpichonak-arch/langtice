using MainService.DAL.Abstractions;
using MainService.DAL.Features.Languages;

namespace MainService.BLL.Data.Languages;

public interface ILanguageRepository : IRepository<Language, Guid>;