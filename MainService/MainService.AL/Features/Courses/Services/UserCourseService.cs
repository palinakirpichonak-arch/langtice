using MainService.AL.Features.Courses.DTO;
using MainService.BLL.Data.Courses;
using MainService.DAL.Features.Courses.Models;
using MainService.IL.Services;

namespace MainService.AL.Features.Courses.Services;

public class UserCourseService : Service<UserCourse, UserCourseDto, UserCourseKey>, IUserCourseService
{
    public UserCourseService(IUserCourseRepository repository) : base(repository) { }

    public async Task<IEnumerable<UserCourse>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var allUserCourses = await _repository.GetAllItemsByAsync(cancellationToken);
        return allUserCourses.Where(uc => uc.UserId == userId);
    }
}