using MainService.AL.Features.Words.DTO;
using MainService.BLL.Data.Words.Repository;
using MainService.DAL.Features.Words.Models;
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

    public async Task<IEnumerable<UserWord>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllItemsAsync(cancellationToken);
        return entities.Where(e => e.UserId == userId);
    }

    public async Task<UserWord?> GetByIdsAsync(Guid userId, Guid wordId, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdsAsync(userId, wordId, cancellationToken);
    }

    public async Task<UserWord> CreateAsync(UserWordDTO dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<UserWord>(dto);
        entity.AddedAt = DateTime.Now;
        await _repository.AddItemAsync(entity, cancellationToken);
        return entity;
    }
    public async Task DeleteAsync(UserWordKey id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetItemByIdAsync(id, cancellationToken);
        if (entity is null) throw new KeyNotFoundException($"UserWord {id} not found");
        await _repository.DeleteItemAsync(entity, cancellationToken);
    }
}