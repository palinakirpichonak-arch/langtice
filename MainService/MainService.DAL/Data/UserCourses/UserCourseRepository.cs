using MainService.DAL.Abstractions;
using MainService.DAL.Context.PostgreSql;
using MainService.DAL.Features.UserCourse;
using Microsoft.EntityFrameworkCore;

namespace MainService.DAL.Data.UserCourses;

public class UserCourseRepository : Repository<UserCourse, UserCourseKey>, IUserCourseRepository
{
    private readonly PostgreDbContext _dbContext;

    public UserCourseRepository(PostgreDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<UserCourse>> GetAllItemsAsync(CancellationToken ct)
    {
        return await _dbContext.UserCourses
            .Include(uc => uc.Course)
            .ToListAsync(ct);
    }

    public async Task<UserCourse?> GetItemByIdAsync(UserCourseKey id, CancellationToken ct)
    {
        return await _dbContext.UserCourses
            .Include(uc => uc.Course)
            .FirstOrDefaultAsync(uc => uc.UserId == id.UserId && uc.CourseId == id.CourseId, ct);
    }
}