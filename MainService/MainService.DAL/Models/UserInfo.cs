using System;
using System.Collections.Generic;

namespace MainService.DAL.Models;

public partial class UserInfo
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string? Period { get; set; }

    public int? WordsLearned { get; set; }

    public int? TestsFinished { get; set; }

    public int? MistakesMade { get; set; }

    public int? StreakLength { get; set; }

    public virtual User User { get; set; } = null!;
}
