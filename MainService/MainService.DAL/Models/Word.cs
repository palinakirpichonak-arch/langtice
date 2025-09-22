using System;
using System.Collections.Generic;

namespace MainService.DAL.Models;

public class Word
{
    public Guid Id { get; set; }
    public string Text { get; set; } = null!;
    public Guid LanguageId { get; set; }
    public virtual Language Language { get; set; } = null!;
    public virtual ICollection<UserWord> UserWords { get; set; } = new List<UserWord>();
    public virtual ICollection<Translation> TranslationsFrom { get; set; } = new List<Translation>();
    public virtual ICollection<Translation> TranslationsTo { get; set; } = new List<Translation>();
}
