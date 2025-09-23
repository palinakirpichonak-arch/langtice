using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MainService.DAL.Models;

public class Word
{
    public Guid Id { get; set; }
    public string Text { get; set; } = null!;
    public Guid? LanguageId { get; set; }
    public Language? Language { get; set; }
    
    [JsonIgnore]
    public ICollection<UserWord>? UserWords { get; set; } = new List<UserWord>();
    
    [JsonIgnore]
    public ICollection<Translation>? TranslationsFrom { get; set; } = new List<Translation>();
    
    [JsonIgnore]
    public ICollection<Translation>? TranslationsTo { get; set; } = new List<Translation>();
}
