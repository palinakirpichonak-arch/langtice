using MainService.AL.Features.Courses.DTO.Request;
using MainService.AL.Features.Courses.DTO.Response;
using MainService.BLL.Services.UnitOfWork;
using MainService.DAL.Data.Courses;
using MainService.DAL.Data.Languages;
using MainService.DAL.Features.Courses;
using MapsterMapper;

namespace MainService.AL.Features.Courses.Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;
    private readonly ILanguageRepository _languageRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CourseService(ICourseRepository courseRepository, ILanguageRepository languageRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _courseRepository = courseRepository;
        _languageRepository = languageRepository;
        _unitOfWork = unitOfWork;
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
        var activeCourses = courses.Where(c => c.Status == true);
        return _mapper.Map<IEnumerable<ResponseCourseDto>>(activeCourses);
    }

    public async Task<ResponseCourseDto> CreateAsync(RequestCourseDto dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Course>(dto);
        entity.Id = Guid.NewGuid();

        entity.BaseLanguage = await _languageRepository.GetItemByIdAsync(dto.BaseLanguageId, cancellationToken);
        entity.LearningLanguage = await _languageRepository.GetItemByIdAsync(dto.LearningLanguageId, cancellationToken);

        if (entity.BaseLanguage is null || entity.LearningLanguage is null)
            throw new KeyNotFoundException("One of the languages was not found");

        _courseRepository.AddItem(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ResponseCourseDto>(entity);
    }

    public async Task<ResponseCourseDto> UpdateAsync(Guid id, RequestCourseDto dto, CancellationToken cancellationToken)
    {
        var entity = await _courseRepository.GetItemByIdAsync(id, cancellationToken);
        if (entity is null)
            throw new KeyNotFoundException($"Course with id {id} not found");

        _mapper.Map(dto, entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ResponseCourseDto>(entity);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _courseRepository.GetItemByIdAsync(id, cancellationToken);
        if (entity is null)
            throw new KeyNotFoundException($"Course with id {id} not found");

        _courseRepository.DeleteItem(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
