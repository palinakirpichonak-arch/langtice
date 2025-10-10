using MainService.AL.Features.Words.DTO.Request;
using MainService.AL.Features.Words.DTO.Response;
using MainService.BLL.Services;
using MainService.DAL.Abstractions;
using MainService.DAL.Features.Words.Models;
using Mapster;
using MapsterMapper;

namespace MainService.AL.Features.Words.Services;

public class UserWordService : IUserWordService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserWordService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseUserWordDto> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.UserWords.GetAllItemsAsync(cancellationToken);

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
        var paginatedEntities = await _unitOfWork.UserWords.GetAllByUserIdAsync(userId, pageIndex, pageSize, cancellationToken);

        var result = (userId, paginatedEntities).Adapt<ResponseUserWordDto>();

        return result;
    }

    public async Task<ResponseUserWordDto?> GetByIdsAsync(Guid userId, Guid wordId, CancellationToken cancellationToken)
    {
        var key = new UserWordKey(userId, wordId);
        var entity = await _unitOfWork.UserWords.GetItemByIdAsync(key, cancellationToken);

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

        _unitOfWork.UserWords.AddItem(entity);
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
        var entity = await _unitOfWork.UserWords.GetItemByIdAsync(key, cancellationToken);
        if (entity is null) throw new KeyNotFoundException($"UserWord {key} not found");

        _unitOfWork.UserWords.DeleteItem(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
