using MainService.AL.Mappers;
using MainService.DAL.Features.Words.Models;

namespace MainService.AL.Features.Words.DTO;

public class UserWordDTO : IMapper<UserWord>
{
    public Guid UserId { get; set; }
    public Guid WordId { get; set; }
    public UserWord ToEntity()
    {
        return new UserWord
        {
            UserId = this.UserId,
            WordId = this.WordId,
            AddedAt = DateTime.UtcNow
        };
    }
    public void ToDto(UserWord entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        entity.UserId = this.UserId;
        entity.WordId = this.WordId;
        entity.AddedAt = DateTime.UtcNow;
    }
}