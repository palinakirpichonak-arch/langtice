using MainService.DAL;
using MainService.DAL.Abstractions;
using MainService.DAL.Context;
using MainService.DAL.Features.Courses.Models;
using Microsoft.EntityFrameworkCore;

namespace MainService.BLL.Data.Courses;

public class UserCourseRepository : Repository<UserCourse, UserCourseKey>, IUserCourseRepository
{
    private readonly PostgreDbContext _dbContext;

    public UserCourseRepository(PostgreDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public override async Task<IEnumerable<UserCourse>> GetAllItemsAsync(CancellationToken ct)
    {
        return await _dbContext.UserCourses
            .Include(uc => uc.Course)
            .ToListAsync(ct);
    }

    public override async Task<UserCourse?> GetItemByIdAsync(UserCourseKey id, CancellationToken ct)
    {
        return await _dbContext.UserCourses
            .Include(uc => uc.Course)
            .FirstOrDefaultAsync(uc => uc.UserId == id.UserId && uc.CourseId == id.CourseId, ct);
    }
}