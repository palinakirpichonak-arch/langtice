using MainService.AL.Exceptions;
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
        var course = (await _courseRepository.GetAsync(
            filter: c => c.Id == id,
            tracking: false,
            cancellationToken: cancellationToken,
            includes:
            [
                c => c.BaseLanguage,
                c => c.LearningLanguage
            ])).FirstOrDefault();
       
        if(course == null)
            throw new NotFoundException("Course not found");
        
        return _mapper.Map<ResponseCourseDto>(course);
    }

    public async Task<IEnumerable<ResponseCourseDto>> GetAllItemsAdminAsync(CancellationToken cancellationToken)
    {
        var courses = await _courseRepository.GetAsync(
            tracking: false,
            cancellationToken: cancellationToken,
            includes: [
                c => c.BaseLanguage,
                c => c.LearningLanguage
            ]);
        
        return _mapper.Map<IEnumerable<ResponseCourseDto>>(courses);
    }

    public async Task<IEnumerable<ResponseCourseDto>> GetActiveCourses(CancellationToken cancellationToken)
    {
        var courses = await _courseRepository.GetAsync(
            filter: c => c.Status == true,
            tracking: false,
            cancellationToken: cancellationToken,
            includes: [
                c => c.BaseLanguage,
                c => c.LearningLanguage
            ]);
       
        return _mapper.Map<IEnumerable<ResponseCourseDto>>(courses);
    }

    public async Task<ResponseCourseDto> CreateAsync(RequestCourseDto dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Course>(dto);
        entity.Id = Guid.NewGuid();
        
        entity.BaseLanguage = (await _languageRepository.GetAsync(
                filter: l => l.Id == dto.BaseLanguageId,
                tracking: false,
                cancellationToken: cancellationToken))
            .FirstOrDefault()!;

        entity.LearningLanguage = (await _languageRepository.GetAsync(
                filter: l => l.Id == dto.LearningLanguageId,
                tracking: false,
                cancellationToken: cancellationToken))
            .FirstOrDefault()!;

        if (entity.BaseLanguage is null || entity.LearningLanguage is null)
            throw new NotFoundException("One of the languages was not found");

        _courseRepository.AddItem(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ResponseCourseDto>(entity);
    }

    public async Task<ResponseCourseDto> UpdateAsync(Guid id, RequestCourseDto dto, CancellationToken cancellationToken)
    {
        var entity = (await _courseRepository.GetAsync(
            filter: c => c.Id == id,
            tracking: true,
            cancellationToken: cancellationToken,
            includes: [
                c => c.BaseLanguage,
                c => c.LearningLanguage
            ])).FirstOrDefault();
        
        if (entity is null)
            throw new NotFoundException($"Course found");

        _mapper.Map(dto, entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ResponseCourseDto>(entity);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _courseRepository.GetAsync(
            filter: c=> c.Id == id, 
            tracking: false,
            cancellationToken: cancellationToken);
        
        if (entity is null)
            throw new NotFoundException($"Course not found");

        _courseRepository.DeleteItem(entity.First());
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
