using MainService.AL.Features.Courses.DTO.Request;
using MainService.AL.Features.Courses.DTO.Response;
using MainService.BLL.Services;
using MainService.DAL.Features.Courses.Models;
using Mapster;
using MapsterMapper;

namespace MainService.AL.Features.Courses.Services;

public class UserCourseService : IUserCourseService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserCourseService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ResponseUserCourseDto>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.UserCourses.GetAllItemsAsync(cancellationToken);
        var filtered = entities.Where(uc => uc.UserId == userId);
        return _mapper.Map<IEnumerable<ResponseUserCourseDto>>(filtered);
    }

    public async Task<ResponseUserCourseDto?> GetByIdsAsync(Guid userId, Guid courseId, CancellationToken cancellationToken)
    {
        var key = new UserCourseKey(userId, courseId);
        var entity = await _unitOfWork.UserCourses.GetItemByIdAsync(key, cancellationToken);
        return entity is null ? null : _mapper.Map<ResponseUserCourseDto>(entity);
    }

    public async Task<ResponseUserCourseDto> CreateAsync(RequestUserCourseDto dto, CancellationToken cancellationToken)
    {
        var entity = dto.Adapt<UserCourse>();
        _unitOfWork.UserCourses.AddItem(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ResponseUserCourseDto>(entity);
    }

    public async Task DeleteAsync(Guid userId, Guid courseId, CancellationToken cancellationToken)
    {
        var key = new UserCourseKey(userId, courseId);
        var entity = await _unitOfWork.UserCourses.GetItemByIdAsync(key, cancellationToken);
        if (entity is null) throw new KeyNotFoundException($"UserCourse {userId}-{courseId} not found");

        _unitOfWork.UserCourses.DeleteItem(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
