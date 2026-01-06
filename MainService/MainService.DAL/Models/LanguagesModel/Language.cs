using MainService.DAL.Abstractions;

namespace MainService.DAL.Models.LanguagesModel
{
    public class Language : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}