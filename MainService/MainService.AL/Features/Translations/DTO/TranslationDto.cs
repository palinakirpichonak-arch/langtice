using MainService.AL.Mappers;
using MainService.DAL.Features.Translations.Models;

namespace MainService.AL.Features.Translations.DTO;

public class TranslationDto : IMapper<Translation>
{
    public Guid FromWordId { get; set; }
    public Guid ToWordId { get; set; }
    public Guid? CourseId { get; set; }

    public Translation ToEntity()
    {
        return new Translation
        {
            FromWordId = FromWordId,
            ToWordId = ToWordId,
            CourseId = CourseId
        };
    }

    public void MapTo(Translation entity)
    {
        entity.FromWordId = FromWordId;
        entity.ToWordId = ToWordId;
        entity.CourseId = CourseId;
    }
}