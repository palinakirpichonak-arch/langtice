using MainService.AL.Features.Lessons.DTO;
using MainService.BLL.Data.Lessons;
using MainService.DAL.Features.Courses.Models;
using MapsterMapper;

namespace MainService.AL.Features.Lessons.Services;

public class LessonService : ILessonService
{
    private readonly ILessonRepository _lessonRepository;
    private readonly ITestRepository _testRepository;
    private readonly IMapper _mapper;

    public LessonService(
        ILessonRepository repository, 
        ITestRepository testRepository,
        IMapper mapper)
    {
        _lessonRepository = repository;
        _testRepository = testRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Lesson>> GetAllByCourseIdAsync(Guid courseId, CancellationToken cancellationToken)
    {
        var lessons = await _lessonRepository.GetAllItemsAsync(cancellationToken);
        return lessons.Where(l => l.CourseId == courseId);
    }

    public async Task<ResponseLessonDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _lessonRepository.GetItemByIdAsync(id, cancellationToken);
        return entity is null ? null : _mapper.Map<ResponseLessonDto>(entity);
    }

    public async Task<IEnumerable<ResponseLessonDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var entities = await _lessonRepository.GetAllItemsAsync(cancellationToken);
        return _mapper.Map<IEnumerable<ResponseLessonDto>>(entities);
    }

    public async Task<ResponseLessonDto> CreateAsync(RequestLessonDto dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Lesson>(dto);
        entity.Id = Guid.NewGuid();
        await _lessonRepository.AddItemAsync(entity, cancellationToken);
        return _mapper.Map<ResponseLessonDto>(entity);
    }

    public async Task<ResponseLessonDto> UpdateAsync(Guid id, RequestLessonDto dto, CancellationToken cancellationToken)
    {
        var entity = await _lessonRepository.GetItemByIdAsync(id, cancellationToken);
        if (entity is null) throw new KeyNotFoundException($"Lesson {id} not found");

        _mapper.Map(dto, entity);
        await _lessonRepository.UpdateItemAsync(entity, cancellationToken);
        return _mapper.Map<ResponseLessonDto>(entity);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _lessonRepository.GetItemByIdAsync(id, cancellationToken);
        if (entity is null) throw new KeyNotFoundException($"Lesson {id} not found");
        
        await _testRepository.DeleteAsync(entity.TestId, cancellationToken);
        await _lessonRepository.DeleteItemAsync(entity, cancellationToken);
    }
}
