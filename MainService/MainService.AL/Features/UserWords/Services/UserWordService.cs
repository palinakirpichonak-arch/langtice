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

    public async Task<ResponseUserWordDto> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var entities = await _userWordRepository.GetAllItemsAsync(cancellationToken);

        var userWordDtos = entities
            .Where(e => e.UserId == userId)
            .Select(e => e.Adapt<UserWordDto>())
            .ToList();
        
        var paginated = new PaginatedList<UserWordDto>(
            items: userWordDtos,
            pageIndex: 1,
            totalPages: userWordDtos.Count == 0 ? 1 : userWordDtos.Count
        );

        return new ResponseUserWordDto
        {
            UserId = userId,
            UserWords = paginated
        };
    }

    public async Task<ResponseUserWordDto> GetAllWithUserIdAsync(Guid userId, int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var paginatedEntities = await _userWordRepository.GetAllByUserIdAsync(userId, pageIndex, pageSize, cancellationToken);

        var result = (userId, paginatedEntities).Adapt<ResponseUserWordDto>();

        return result;
    }

    public async Task<ResponseUserWordDto?> GetByIdsAsync(Guid userId, Guid wordId, CancellationToken cancellationToken)
    {
        var key = new UserWordKey(userId, wordId);
        var entity = await _userWordRepository.GetItemByIdAsync(key, cancellationToken);

        if (entity is null) return null;

        var dto = entity.Adapt<UserWordDto>();
        var paginated = new PaginatedList<UserWordDto>(new List<UserWordDto> { dto }, 1, 1);

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
        var paginated = new PaginatedList<UserWordDto>(new List<UserWordDto> { userWordDto }, 1, 1);

        return new ResponseUserWordDto
        {
            UserId = entity.UserId,
            UserWords = paginated
        };
    }

    public async Task DeleteAsync(Guid userId, Guid wordId, CancellationToken cancellationToken)
    {
        var key = new UserWordKey(userId, wordId);
        var entity = await _userWordRepository.GetItemByIdAsync(key, cancellationToken);
        if (entity is null) throw new KeyNotFoundException($"UserWord {key} not found");

        _userWordRepository.DeleteItem(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
