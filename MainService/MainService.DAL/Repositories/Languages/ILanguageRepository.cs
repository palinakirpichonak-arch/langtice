using MainService.DAL.Abstractions;
using MainService.DAL.Models.LanguagesModel;

namespace MainService.DAL.Repositories.Language_;

public interface ILanguageRepository : IRepository<Language, Guid>;