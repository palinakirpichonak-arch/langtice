using System;
using System.Collections.Generic;

namespace MainService.DAL.Models;

public class Translation
{
    public Guid Id { get; set; }
    public Guid WordId { get; set; }
    public virtual Word Word { get; set; } = null!;
    public string TranslationText { get; set; } = null!;
    public Guid TargetLanguageId { get; set; }
    public virtual Language TargetLanguage { get; set; } = null!;
}
