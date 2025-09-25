using MainService.AL.Mappers;
using MainService.DAL.Features.Words.Models;

namespace MainService.AL.Features.Words.DTO;

public class WordDto : IMapper<Word>
{
    public string Text { get; set; } = null!;
    public Guid? LanguageId { get; set; }
    public Word ToEntity()
    {
        return new Word
        {
            Id = Guid.NewGuid(),
            Text = this.Text,
            LanguageId = this.LanguageId
        };
    }

    public void MapTo(Word entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        entity.Text = this.Text;
        entity.LanguageId = this.LanguageId;
    }
}