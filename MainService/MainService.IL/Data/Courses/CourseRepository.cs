using MainService.DAL.Abstractions;
using MainService.DAL.Context.PostgreSql;
using MainService.DAL.Features.Courses;
using Microsoft.EntityFrameworkCore;

namespace MainService.BLL.Data.Courses;

public class CourseRepository : Repository<Course, Guid>,ICourseRepository
{
    public CourseRepository(PostgreDbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<Course?> GetItemByIdAsync(Guid id, CancellationToken ct)
    {
        return await _dbContext.Courses
            .Include(c => c.BaseLanguage)
            .Include(c => c.LearningLanguage)
            .FirstOrDefaultAsync(c => c.Id == id, ct);
    }

    public override async Task<IEnumerable<Course>> GetAllItemsAsync(CancellationToken ct)
    {
        return await _dbContext.Courses
            .Include(c => c.BaseLanguage)
            .Include(c => c.LearningLanguage)
            .ToListAsync(ct);
    }
    
}