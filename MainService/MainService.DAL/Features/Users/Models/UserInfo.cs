using System;
using System.Collections.Generic;
using MainService.DAL.Abstractions;
using MainService.DAL.Features.Users.Models;

namespace MainService.DAL.Models;

public class UserInfo : IEntity<Guid>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public DateTime AddedAt { get; set; }
    public DateTime? Period { get; set; }
    public int? WordsLearned { get; set; } = 0;
    public int? TestsFinished { get; set; } = 0;
    public int? MistakesMade { get; set; } = 0;
    public int? StreakLength { get; set; } = 0;
}
