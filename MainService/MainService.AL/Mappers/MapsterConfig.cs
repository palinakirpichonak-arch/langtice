using MainService.AL.Features.Courses.DTO.Request;
using MainService.AL.Features.Courses.DTO.Response;
using MainService.AL.Features.Translations.DTO.Response;
using MainService.AL.Features.Words.DTO.Request;
using MainService.AL.Features.Words.DTO.Response;
using MainService.DAL.Abstractions;
using MainService.DAL.Features.Courses.Models;
using MainService.DAL.Features.Translations.Models;
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
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.UserWords, src => new List<UserWordDto> { src.Adapt<UserWordDto>() });
        TypeAdapterConfig<UserWord, UserWordDto>.NewConfig()
            .Map(dest=>dest.Id, src=>src.Id.WordId)
            .Map(dest => dest.Word, src => src.Word.Text)
            .Map(dest => dest.AddedAt, src => src.AddedAt);
        TypeAdapterConfig<(Guid UserId, PaginatedList<UserWord> UserWords), ResponseUserWordDto>.NewConfig()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.UserWords, src => 
                new PaginatedList<UserWordDto>(
                    src.UserWords.Items.Adapt<List<UserWordDto>>(),
                    src.UserWords.PageIndex,
                    src.UserWords.TotalPages
                )
            );
        TypeAdapterConfig<Word, ResponseWordDto>.NewConfig();
        TypeAdapterConfig<Translation, ResponseTranslationDto>.NewConfig()
            .Map(dest => dest.FromWord, src => src.FromWord.Adapt<ResponseWordDto>())
            .Map(dest => dest.ToWord, src => src.ToWord.Adapt<ResponseWordDto>());
        TypeAdapterConfig<RequestUserCourseDto, UserCourse>.NewConfig()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.CourseId, src => src.CourseId);
        TypeAdapterConfig<UserCourse, ResponseUserCourseDto>.NewConfig()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.CourseId, src => src.CourseId);
    }
}