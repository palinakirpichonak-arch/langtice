using System;
using System.Collections.Generic;

namespace MainService.DAL.Models;

public partial class Word
{
    public Guid Id { get; set; }

    public string Text { get; set; } = null!;

    public Guid LanguageId { get; set; }

    public Guid? UserId { get; set; }

    public DateTime? AddedAt { get; set; }

    public string Type { get; set; } = null!;

    public virtual Language Language { get; set; } = null!;

    public virtual ICollection<Mistake> Mistakes { get; set; } = new List<Mistake>();

    public virtual ICollection<Translation> Translations { get; set; } = new List<Translation>();

    public virtual User? User { get; set; }
}
