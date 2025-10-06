using MainService.AL.Features.Courses.DTO.Request;
using MainService.AL.Features.Courses.DTO.Response;
using MainService.BLL.Data.Courses;
using MainService.DAL.Features.Courses.Models;
using Mapster;
using MapsterMapper;

namespace MainService.AL.Features.Courses.Services;

public class UserCourseService : IUserCourseService
{
    private readonly IUserCourseRepository _repository;
    private readonly IMapper _mapper;

    public UserCourseService(IUserCourseRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ResponseUserCourseDto>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllItemsAsync(cancellationToken);
        var filtered = entities.Where(uc => uc.UserId == userId);
        return _mapper.Map<IEnumerable<ResponseUserCourseDto>>(filtered);
    }
    
    public async Task<ResponseUserCourseDto?> GetByIdsAsync(Guid userId, Guid courseId, CancellationToken cancellationToken)
    {
        var key = new UserCourseKey(userId, courseId);
        var entity = await _repository.GetItemByIdAsync(key, cancellationToken);
        return entity is null ? null : _mapper.Map<ResponseUserCourseDto>(entity);
    }
    
    public async Task<ResponseUserCourseDto> CreateAsync(RequestUserCourseDto dto, CancellationToken cancellationToken)
    {
        var entity = dto.Adapt<UserCourse>();
        await _repository.AddItemAsync(entity, cancellationToken);
        var response = entity.Adapt<ResponseUserCourseDto>();

        return response;
    }

    public async Task DeleteAsync(Guid userId, Guid courseId, CancellationToken cancellationToken)
    {
        var key = new UserCourseKey(userId, courseId); ;
        var entity = await _repository.GetItemByIdAsync(key, cancellationToken);
        if (entity is null) throw new KeyNotFoundException($"UserCourse {userId}-{courseId} not found");
        await _repository.DeleteItemAsync(entity, cancellationToken);
    }
}
