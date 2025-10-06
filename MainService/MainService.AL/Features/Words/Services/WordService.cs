using MainService.AL.Features.Words.DTO.Request;
using MainService.AL.Features.Words.DTO.Response;
using MainService.BLL.Data.Words.Repository;
using MainService.DAL.Features.Words.Models;
using MapsterMapper;

namespace MainService.AL.Features.Words.Services;

public class WordService : IWordService
{
    private readonly IWordRepository _repository;
    private readonly IMapper _mapper;

    public WordService(IWordRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseWordDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetItemByIdAsync(id, cancellationToken);
        return entity is null ? null : _mapper.Map<ResponseWordDto>(entity);
    }

    public async Task<IEnumerable<ResponseWordDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllItemsAsync(cancellationToken);
        return _mapper.Map<IEnumerable<ResponseWordDto>>(entities);
    }

    public async Task<ResponseWordDto> CreateAsync(RequestWordDto dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Word>(dto);
        entity.Id = Guid.NewGuid();
        await _repository.AddItemAsync(entity, cancellationToken);
        return _mapper.Map<ResponseWordDto>(entity);
    }

    public async Task<ResponseWordDto> UpdateAsync(Guid id, RequestWordDto dto, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetItemByIdAsync(id, cancellationToken);
        if (entity is null) throw new KeyNotFoundException($"Word {id} not found");

        _mapper.Map(dto, entity);
        await _repository.UpdateItemAsync(entity, cancellationToken);
        return _mapper.Map<ResponseWordDto>(entity);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetItemByIdAsync(id, cancellationToken);
        if (entity is null) throw new KeyNotFoundException($"Word {id} not found");

        await _repository.DeleteItemAsync(entity, cancellationToken);
    }
}
