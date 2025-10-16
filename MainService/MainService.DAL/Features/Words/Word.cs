using System.Text.Json.Serialization;
using MainService.DAL.Abstractions;
using MainService.DAL.Features.Languages;

namespace MainService.DAL.Features.Words;

public class Word : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Text { get; set; } = null!;
    public Guid LanguageId { get; set; }
    public Language Language { get; set; }
    
    [JsonIgnore]
    public ICollection<UserWord.UserWord>? UserWords { get; set; } = new List<UserWord.UserWord>();
}
