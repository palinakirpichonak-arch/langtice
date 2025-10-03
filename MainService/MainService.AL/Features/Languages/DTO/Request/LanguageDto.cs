using MainService.AL.Mappers;
using MainService.DAL.Features.Languages.Models;

namespace MainService.AL.Features.Languages.DTO
{
    public class LanguageDto : IMapper<Language>
    {
        public string Name { get; set; } = null!;
        
        public Language ToEntity()
        {
            return new Language
            {
                Id = Guid.NewGuid(),
                Name = this.Name
            };
        }
        
        public void ToDto(Language entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            entity.Name = this.Name;
        }
    }
}