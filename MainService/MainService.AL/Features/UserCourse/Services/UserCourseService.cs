using MainService.AL.Exceptions;
using MainService.AL.Features.UserCourse.DTO.Request;
using MainService.AL.Features.UserCourse.DTO.Response;
using MainService.BLL.Services.UnitOfWork;
using MainService.DAL.Data.UserCourses;
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
        var entities = await _userCourseRepository.GetAsync(
            filter: uc => uc.UserId == userId,
            tracking: false,
            cancellationToken: cancellationToken);

        return _mapper.Map<IEnumerable<ResponseUserCourseDto>>(entities);
    }

    public async Task<ResponseUserCourseDto?> GetByIdsAsync(Guid userId, Guid courseId, CancellationToken cancellationToken)
    {
        var entities = await _userCourseRepository.GetAsync(
            filter: uc => uc.UserId == userId && uc.CourseId == courseId,
            tracking: false,
            cancellationToken: cancellationToken);

        var entity = entities.FirstOrDefault();
        if (entity is null)
            throw new NotFoundException("User course not found");

        return _mapper.Map<ResponseUserCourseDto>(entity);
    }

    public async Task<ResponseUserCourseDto> CreateAsync(RequestUserCourseDto dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<DAL.Features.UserCourse.UserCourse>(dto);

        _userCourseRepository.AddItem(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ResponseUserCourseDto>(entity);
    }

    public async Task DeleteAsync(Guid userId, Guid courseId, CancellationToken cancellationToken)
    {
        var entities = await _userCourseRepository.GetAsync(
            filter: uc => uc.UserId == userId && uc.CourseId == courseId,
            tracking: false,
            cancellationToken: cancellationToken);

        var entity = entities.FirstOrDefault();
        if (entity is null)
            throw new NotFoundException("User course not found");

        _userCourseRepository.DeleteItem(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
