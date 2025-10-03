using MainService.AL.Mappers;
using MainService.DAL.Features.Courses.Models;

namespace MainService.AL.Features.Courses.DTO;

public class UserCourseDto : IMapper<UserCourse>
{
    public Guid UserId { get; set; }
    public Guid CourseId { get; set; }

    public UserCourse ToEntity()
    {
        return new UserCourse
        {
            UserId = UserId,
            CourseId = CourseId,
        };
    }

    public void ToDto(UserCourse entity)
    {
        entity.UserId = UserId;
        entity.CourseId = CourseId;
    }
}