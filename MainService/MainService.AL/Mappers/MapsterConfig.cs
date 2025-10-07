using MainService.AL.Features.Courses.DTO.Request;
using MainService.AL.Features.Courses.DTO.Response;
using MainService.AL.Features.Words.DTO.Request;
using MainService.AL.Features.Words.DTO.Response;
using MainService.DAL.Features.Courses.Models;
using MainService.DAL.Features.Words.Models;
using Mapster;

namespace MainService.AL.Mappers;

public static class MapsterConfig
{
    public static void Configure()
    {
        TypeAdapterConfig<RequestUserWordDto, UserWord>.NewConfig()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.WordId, src => src.WordId)
            .Map(dest => dest.AddedAt, src => DateTime.UtcNow);
        TypeAdapterConfig<UserWord, ResponseUserWordDto>.NewConfig()
            .Map(dest => dest.Word.Id, src => src.WordId)
            .Map(dest => dest.Word.Text, src => src.Word.Text);
        TypeAdapterConfig<RequestUserCourseDto, UserCourse>.NewConfig()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.CourseId, src => src.CourseId);
        TypeAdapterConfig<UserCourse, ResponseUserCourseDto>.NewConfig()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.CourseId, src => src.CourseId);
    }
}