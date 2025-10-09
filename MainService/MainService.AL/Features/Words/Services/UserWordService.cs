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

    public async Task<IEnumerable<ResponseUserWordDto>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.UserWords.GetAllItemsAsync(cancellationToken);
        return entities
            .Where(e => e.UserId == userId)
            .Select(e => e.Adapt<ResponseUserWordDto>());
    }

    public async Task<PaginatedList<ResponseUserWordDto>> GetAllWithUserIdAsync(Guid userId, int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.UserWords.GetAllByUserIdAsync(userId, pageIndex, pageSize, cancellationToken);
        var list = entities.Items.Adapt<List<ResponseUserWordDto>>();
        return new PaginatedList<ResponseUserWordDto>(list, pageIndex, pageSize);
    }

    public async Task<ResponseUserWordDto?> GetByIdsAsync(Guid userId, Guid wordId, CancellationToken cancellationToken)
    {
        var key = new UserWordKey(userId, wordId);
        var entity = await _unitOfWork.UserWords.GetItemByIdAsync(key, cancellationToken);
        return entity?.Adapt<ResponseUserWordDto>();
    }

    public async Task<ResponseUserWordDto> CreateAsync(RequestUserWordDto dto, CancellationToken cancellationToken)
    {
        var entity = dto.Adapt<UserWord>();
        entity.AddedAt = DateTime.UtcNow;

        _unitOfWork.UserWords.AddItem(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Adapt<ResponseUserWordDto>();
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
