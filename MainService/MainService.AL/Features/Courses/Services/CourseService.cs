using MainService.AL.Features.Courses.DTO.Request;
using MainService.AL.Features.Courses.DTO.Response;
using MainService.BLL.Data.Courses;
using MainService.BLL.Data.Languages;
using MainService.DAL.Features.Courses.Models;
using MapsterMapper;

namespace MainService.AL.Features.Courses.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly ILanguageRepository _languageRepository;
    private readonly IMapper _mapper;

    public CourseService(
        ICourseRepository repository, 
        ILanguageRepository languageRepository,
        IMapper mapper)
    {
        _courseRepository = repository;
        _languageRepository = languageRepository;
        _mapper = mapper;
    }

    public async Task<ResponseCourseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetItemByIdAsync(id, cancellationToken);
        return course is null ? null : _mapper.Map<ResponseCourseDto>(course);
    }

    public async Task<IEnumerable<ResponseCourseDto>> GetAllItemsAdminAsync(CancellationToken cancellationToken)
    {
        var courses = await _courseRepository.GetAllItemsAsync(cancellationToken);
        return _mapper.Map<IEnumerable<ResponseCourseDto>>(courses);
    }

    public async Task<IEnumerable<ResponseCourseDto>> GetActiveCourses(CancellationToken cancellationToken)
    {
        var courses = await _courseRepository.GetAllItemsAsync(cancellationToken);
        var userCourses = courses
                                        .Select(c => c)
                                        .Where(c => c.Status == true)
                                        .ToList();
        return _mapper.Map<IEnumerable<ResponseCourseDto>>(userCourses);
    }

    public async Task<ResponseCourseDto> CreateAsync(RequestCourseDto dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Course>(dto);
        entity.Id = Guid.NewGuid();
        entity.BaseLanguage = await _languageRepository.GetItemByIdAsync(dto.BaseLanguageId, cancellationToken);
        entity.LearningLanguage = await _languageRepository.GetItemByIdAsync(dto.LearningLanguageId, cancellationToken);
        await _courseRepository.AddItemAsync(entity, cancellationToken);
        
        return _mapper.Map<ResponseCourseDto>(entity);
    }

    public async Task<ResponseCourseDto> UpdateAsync(Guid id, RequestCourseDto dto, CancellationToken cancellationToken)
    {
        var entity = await _courseRepository.GetItemByIdAsync(id, cancellationToken);
        if (entity is null)
            throw new KeyNotFoundException($"Course with id {id} not found");

        _mapper.Map(dto, entity);

        return _mapper.Map<ResponseCourseDto>(entity);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _courseRepository.GetItemByIdAsync(id, cancellationToken);
        if (entity is null)
            throw new KeyNotFoundException($"Course with id {id} not found");

        await _courseRepository.DeleteItemAsync(entity, cancellationToken);
    }
}
