using MainService.AL.Features.Lessons.DTO.Request;
using MainService.AL.Features.Lessons.DTO.Response;
using MainService.BLL.Data.Courses;
using MainService.BLL.Services;
using MainService.DAL.Abstractions;
using MainService.DAL.Features.Lessons;
using Mapster;
using MapsterMapper;

namespace MainService.AL.Features.Lessons.Services;

public class LessonService : ILessonService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITestRepository _testRepository;
    private readonly IMapper _mapper;

    public LessonService(IUnitOfWork unitOfWork, ITestRepository testRepository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _testRepository = testRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Lesson>> GetAllByCourseIdAsync(Guid courseId, CancellationToken cancellationToken)
    {
        var lessons = await _unitOfWork.Lessons.GetAllItemsAsync(cancellationToken);
        return lessons.Where(l => l.CourseId == courseId);
    }

    public async Task<ResponseLessonDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Lessons.GetItemByIdAsync(id, cancellationToken);
        return entity is null ? null : _mapper.Map<ResponseLessonDto>(entity);
    }

    public async Task<PaginatedList<ResponseLessonDto>> GetAllAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.Lessons.GetAllItemsAsync(pageIndex, pageSize, cancellationToken);
        var list = entities.Items.Adapt<List<ResponseLessonDto>>();
        return new PaginatedList<ResponseLessonDto>(list, pageIndex, pageSize);
    }

    public async Task<PaginatedList<ResponseLessonDto>> GetAllWithCourseIdAsync(Guid courseId, int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.Lessons.GetAllItemsWithIdAsync(courseId, pageIndex, pageSize, cancellationToken);
        var list = entities.Items.Adapt<List<ResponseLessonDto>>();
        return new PaginatedList<ResponseLessonDto>(list, pageIndex, pageSize);
    }

    public async Task<ResponseLessonDto> CreateAsync(RequestLessonDto dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Lesson>(dto);
        entity.Id = Guid.NewGuid();

        _unitOfWork.Lessons.AddItem(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ResponseLessonDto>(entity);
    }

    public async Task<ResponseLessonDto> UpdateAsync(Guid id, RequestLessonDto dto, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Lessons.GetItemByIdAsync(id, cancellationToken);
        if (entity is null) throw new KeyNotFoundException($"Lesson {id} not found");

        _mapper.Map(dto, entity);
        _unitOfWork.Lessons.UpdateItem(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ResponseLessonDto>(entity);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Lessons.GetItemByIdAsync(id, cancellationToken);
        if (entity is null) throw new KeyNotFoundException($"Lesson {id} not found");

        await _testRepository.DeleteAsync(entity.TestId, cancellationToken);

        _unitOfWork.Lessons.DeleteItem(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
