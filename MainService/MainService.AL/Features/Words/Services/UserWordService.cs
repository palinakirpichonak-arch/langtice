using MainService.AL.Features.Words.DTO.Request;
using MainService.AL.Features.Words.DTO.Response;
using MainService.BLL.Data.Words.Repository;
using MainService.DAL.Features.Words.Models;
using Mapster;
using MapsterMapper;

namespace MainService.AL.Features.Words.Services;

public class UserWordService : IUserWordService
{
    private readonly IUserWordRepository _repository;
    private readonly IMapper _mapper;

    public UserWordService(IUserWordRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ResponseUserWordDto>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllItemsAsync(cancellationToken);
        return entities
            .Where(e => e.UserId == userId)
            .Select(e => e.Adapt<ResponseUserWordDto>());
    }

    public async Task<ResponseUserWordDto?> GetByIdsAsync(Guid userId, Guid wordId, CancellationToken cancellationToken)
    {
        UserWordKey key = new(userId, wordId);
        var entity = await _repository.GetItemByIdAsync(key, cancellationToken);
        return entity?.Adapt<ResponseUserWordDto>();
    }

    public async Task<ResponseUserWordDto> CreateAsync(RequestUserWordDto dto, CancellationToken cancellationToken)
    {
        var entity = dto.Adapt<UserWord>();
        entity.AddedAt = DateTime.UtcNow;

        await _repository.AddItemAsync(entity, cancellationToken);

        return entity.Adapt<ResponseUserWordDto>();
    }
    public async Task DeleteAsync(Guid userId, Guid wordId, CancellationToken cancellationToken)
    {
        UserWordKey key = new UserWordKey(userId, wordId);
        var entity = await _repository.GetItemByIdAsync(key, cancellationToken);
        if (entity is null) throw new KeyNotFoundException($"UserWord {key} not found");
        await _repository.DeleteItemAsync(entity, cancellationToken);
    }
}