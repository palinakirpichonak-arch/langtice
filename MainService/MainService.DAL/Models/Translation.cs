using System;
using System.Collections.Generic;

namespace MainService.DAL.Models;

public partial class Translation
{
    public Guid Id { get; set; }

    public Guid WordId { get; set; }

    public string TranslationText { get; set; } = null!;

    public Guid TargetLanguageId { get; set; }

    public virtual Language TargetLanguage { get; set; } = null!;

    public virtual Word Word { get; set; } = null!;
}
