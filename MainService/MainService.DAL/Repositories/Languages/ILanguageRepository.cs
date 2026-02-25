using MainService.DAL.Abstractions;
using MainService.DAL.Models.LanguagesModel;

namespace MainService.DAL.Repositories.Languages;

public interface ILanguageRepository : IRepository<Language, Guid>;