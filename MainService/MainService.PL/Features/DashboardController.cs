using MainService.AL.Features.UserStreaks;
using MainService.PL.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace MainService.PL.Features;

[ApiController]
public class DashboardController : ControllerBase
{
    private readonly IUserStreakService _userStreakService;

    public DashboardController(IUserStreakService userStreakService)
    {
        _userStreakService = userStreakService;
    }
    
    [HttpGet]
    [Route("[controller]")]
    public async Task<IActionResult> GetStreak(CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();
        var nowUtc = DateTime.UtcNow;
        await _userStreakService.TrackUserVisitAsync(userId,nowUtc, cancellationToken);
        
        return Ok();
    }
}