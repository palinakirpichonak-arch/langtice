using MainService.AL.Features.Courses.DTO;
using MainService.AL.Features.Courses.DTO.Response;
using MainService.BLL.Data.Courses;
using MainService.DAL.Features.Courses.Models;
using MapsterMapper;

namespace MainService.AL.Features.Courses.Services;

public class UserCourseService : IUserCourseService
{
    private readonly IUserCourseRepository _courseRepository;
    private readonly IMapper _mapper;

    public UserCourseService(IUserCourseRepository repository, IMapper mapper)
    {
        _courseRepository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseUserCourseDto?> GetByIdAsync(UserCourseKey id, CancellationToken cancellationToken)
    {
        var entity = await _courseRepository.GetItemByIdAsync(id, cancellationToken);
        return entity is null ? null : _mapper.Map<ResponseUserCourseDto>(entity);
    }

    public async Task<IEnumerable<ResponseUserCourseDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var entities = await _courseRepository.GetAllItemsAsync(cancellationToken);
        return _mapper.Map<IEnumerable<ResponseUserCourseDto>>(entities);
    }

    public async Task<IEnumerable<ResponseUserCourseDto>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var entities = await _courseRepository.GetAllItemsAsync(cancellationToken);
        var filtered = entities.Where(uc => uc.UserId == userId);
        return _mapper.Map<IEnumerable<ResponseUserCourseDto>>(filtered);
    }

    public async Task<ResponseUserCourseDto> CreateAsync(UserCourseDto dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<UserCourse>(dto);
        

        await _courseRepository.AddItemAsync(entity, cancellationToken);

        return _mapper.Map<ResponseUserCourseDto>(entity);
    }

    public async Task<ResponseUserCourseDto> UpdateAsync(UserCourseKey id, UserCourseDto dto, CancellationToken cancellationToken)
    {
        var entity = await _courseRepository.GetItemByIdAsync(id, cancellationToken);
        if (entity is null)
            throw new KeyNotFoundException($"UserCourse with id {id} not found");

        _mapper.Map(dto, entity);

        await _courseRepository.UpdateItemAsync(entity, cancellationToken);

        return _mapper.Map<ResponseUserCourseDto>(entity);
    }

    public async Task DeleteAsync(UserCourseKey id, CancellationToken cancellationToken)
    {
        var entity = await _courseRepository.GetItemByIdAsync(id, cancellationToken);
        if (entity is null)
            throw new KeyNotFoundException($"UserCourse with id {id} not found");

        await _courseRepository.DeleteItemAsync(entity, cancellationToken);
    }
}
