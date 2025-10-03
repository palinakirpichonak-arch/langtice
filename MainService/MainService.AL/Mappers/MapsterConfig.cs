using MainService.AL.Features.Courses.DTO;
using MainService.DAL.Features.Courses.Models;
using Mapster;

namespace MainService.AL.Mappers;

public class MapsterConfig
{
    public static void Configure()
    {
        TypeAdapterConfig<Course, CourseDto>.NewConfig();
    }
}