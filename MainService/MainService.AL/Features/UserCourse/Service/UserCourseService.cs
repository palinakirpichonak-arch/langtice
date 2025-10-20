using MainService.AL.Features.UserCourse.DTO.Request;
using MainService.AL.Features.UserCourse.DTO.Response;
using MainService.BLL.Data.UserCourses;
using MainService.BLL.Services.UnitOfWork;
using MainService.DAL.Features.UserCourse;
using Mapster;
using MapsterMapper;

namespace MainService.AL.Features.UserCourse.Service;

public class UserCourseService : IUserCourseService
{
    private readonly IUserCourseRepository _userCourseRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserCourseService(
        IUserCourseRepository userCourseRepository,
        IUnitOfWork unitOfWork, 
        IMapper mapper)
    {
        _userCourseRepository = userCourseRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ResponseUserCourseDto>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var entities = await _userCourseRepository.GetAllItemsAsync(cancellationToken);
        var filtered = entities.Where(uc => uc.UserId == userId);
        return _mapper.Map<IEnumerable<ResponseUserCourseDto>>(filtered);
    }

    public async Task<ResponseUserCourseDto?> GetByIdsAsync(Guid userId, Guid courseId, CancellationToken cancellationToken)
    {
        var key = new UserCourseKey(userId, courseId);
        var entity = await _userCourseRepository.GetItemByIdAsync(key, cancellationToken);
        return entity is null ? null : _mapper.Map<ResponseUserCourseDto>(entity);
    }

    public async Task<ResponseUserCourseDto> CreateAsync(RequestUserCourseDto dto, CancellationToken cancellationToken)
    {
        var entity = dto.Adapt<DAL.Features.UserCourse.UserCourse>();
        _userCourseRepository.AddItem(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ResponseUserCourseDto>(entity);
    }

    public async Task DeleteAsync(Guid userId, Guid courseId, CancellationToken cancellationToken)
    {
        var key = new UserCourseKey(userId, courseId);
        var entity = await _userCourseRepository.GetItemByIdAsync(key, cancellationToken);
        if (entity is null) throw new KeyNotFoundException($"UserCourse {userId}-{courseId} not found");

        _userCourseRepository.DeleteItem(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
