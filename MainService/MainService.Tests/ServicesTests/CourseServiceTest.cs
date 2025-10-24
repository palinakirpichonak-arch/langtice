using System.Linq.Expressions;
using MainService.AL.Features.Courses.DTO.Response;
using MainService.AL.Features.Courses.Services;
using MainService.BLL.Services.UnitOfWork;
using MainService.DAL.Data.Courses;
using MainService.DAL.Data.Languages;
using MainService.DAL.Features.Courses;
using MapsterMapper;
using Moq;

namespace MainService.Tests.ServicesTests;

public class CourseServiceTest
{
    private readonly Mock<ICourseRepository> _courseRepoMock = new();
    private readonly Mock<ILanguageRepository> _languageRepoMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    
    private CourseService _courseService;
    private CancellationToken _cancellationToken = CancellationToken.None;

    
    [Fact]
    public async Task GetCourseById_ShouldReturnResponseCourseDto()
    {   
        var testCourse = new Course
        {
            Id = Guid.NewGuid(),
            BaseLanguageId = Guid.NewGuid(),
            LearningLanguageId = Guid.NewGuid(),
        };
    
        _courseRepoMock
            .Setup(r => r.GetAsync(
                It.IsAny<Expression<Func<Course, bool>>>(),
                null,
                It.IsAny<int?>(),
                It.IsAny<int?>(),
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>(),
                null))
            .ReturnsAsync(new List<Course> { testCourse });
    
        _mapperMock
            .Setup(m => m.Map<ResponseCourseDto>(It.IsAny<Course>()))
            .Returns((Course src) => new ResponseCourseDto { Id = src.Id });
        
        _courseService = new CourseService(_courseRepoMock.Object, _languageRepoMock.Object, _unitOfWorkMock.Object, _mapperMock.Object);

        var result = await _courseService.GetByIdAsync(testCourse.Id, _cancellationToken);
    
        Assert.NotNull(result);
        Assert.Equal(testCourse.Id, result.Id);
    }
}