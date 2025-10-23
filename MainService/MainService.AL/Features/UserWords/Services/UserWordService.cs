using MainService.AL.Exceptions;
using MainService.AL.Features.UserWords.DTO.Request;
using MainService.AL.Features.UserWords.DTO.Response;
using MainService.BLL.Services.UnitOfWork;
using MainService.DAL.Abstractions;
using MainService.DAL.Data.UserWord;
using MainService.DAL.Features.UserWord;
using Mapster;

namespace MainService.AL.Features.UserWords.Services;

public class UserWordService : IUserWordService
{
    private readonly IUserWordRepository _userWordRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserWordService(
        IUserWordRepository userWordRepository,
        IUnitOfWork unitOfWork)
    {
        _userWordRepository = userWordRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseUserWordDto> GetAllWithUserIdAsync(
        Guid userId,
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var entities = await _userWordRepository.GetAsync(
            filter: uw => uw.UserId == userId,
            tracking: false,
            pageIndex: pageIndex,
            pageSize: pageSize,
            cancellationToken: cancellationToken);

        var userWordDtos = entities.Adapt<List<UserWordDto>>();

        return new ResponseUserWordDto
        {
            UserId = userId,
            UserWords = userWordDtos
        };
    }

    public async Task<ResponseUserWordDto?> GetByIdsAsync(Guid userId, Guid wordId, CancellationToken cancellationToken)
    {
        var entities = await _userWordRepository.GetAsync(
            filter: uw => uw.UserId == userId && uw.WordId == wordId,
            tracking: false,
            cancellationToken: cancellationToken);

        var entity = entities.FirstOrDefault();
        if (entity is null)
            throw new NotFoundException("User word not found");

        var userWordDto = entity.Adapt<UserWordDto>();
        var paginated = new List<UserWordDto> {userWordDto};

        return new ResponseUserWordDto
        {
            UserId = userId,
            UserWords = paginated
        };
    }

    public async Task<ResponseUserWordDto> CreateAsync(RequestUserWordDto dto, CancellationToken cancellationToken)
    {
        var entity = dto.Adapt<UserWord>();
        entity.AddedAt = DateTime.UtcNow;

        _userWordRepository.AddItem(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var userWordDto = entity.Adapt<UserWordDto>();
        var paginated = new List<UserWordDto> {userWordDto};

        return new ResponseUserWordDto
        {
            UserId = entity.UserId,
            UserWords = paginated
        };
    }

    public async Task DeleteAsync(Guid userId, Guid wordId, CancellationToken cancellationToken)
    {
        var entities = await _userWordRepository.GetAsync(
            filter: uw => uw.UserId == userId && uw.WordId == wordId,
            tracking: false,
            cancellationToken: cancellationToken);

        var entity = entities.FirstOrDefault();
        if (entity is null)
            throw new NotFoundException($"UserWord not found");

        _userWordRepository.DeleteItem(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
