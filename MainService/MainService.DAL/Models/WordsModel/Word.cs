using System.Text.Json.Serialization;
using MainService.DAL.Abstractions;
using MainService.DAL.Models.LanguagesModel;
using MainService.DAL.Models.UserWordModel;

namespace MainService.DAL.Models.WordsModel
{
    public class Word : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = null!;
        public Guid LanguageId { get; set; }
        public Language Language { get; set; }
    
        [JsonIgnore]
        public ICollection<UserWord>? UserWords { get; set; } = new List<UserWord>();
    }
}

