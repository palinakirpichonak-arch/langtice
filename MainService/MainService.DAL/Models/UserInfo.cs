using System;
using System.Collections.Generic;

namespace MainService.DAL.Models;

public class UserInfo
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public virtual User User { get; set; } = null!;
    public DateTime AddedAt { get; set; }
    public DateTime? Period { get; set; }
    public int? WordsLearned { get; set; } = 0;
    public int? TestsFinished { get; set; } = 0;
    public int? MistakesMade { get; set; } = 0;
    public int? StreakLength { get; set; } = 0;
}
