using MainService.AL.Features.Languages.DTO;
using MainService.BLL.Data.Languages;
using MainService.DAL.Features.Languages.Models;
using MapsterMapper;

namespace MainService.AL.Features.Languages.Services;

public class LanguageService : ILanguageService
{
    private readonly ILanguageRepository _repository;
    private readonly IMapper _mapper;

    public LanguageService(ILanguageRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseLanguageDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetItemByIdAsync(id, cancellationToken);
        return entity is null ? null : _mapper.Map<ResponseLanguageDto>(entity);
    }

    public async Task<IEnumerable<ResponseLanguageDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllItemsAsync(cancellationToken);
        return _mapper.Map<IEnumerable<ResponseLanguageDto>>(entities);
    }

    public async Task<ResponseLanguageDto> CreateAsync(RequestLanguageDto dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Language>(dto);
        entity.Id = Guid.NewGuid();

        await _repository.AddItemAsync(entity, cancellationToken);

        return _mapper.Map<ResponseLanguageDto>(entity);
    }

    public async Task<ResponseLanguageDto> UpdateAsync(Guid id, ResponseLanguageDto dto, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetItemByIdAsync(id, cancellationToken);
        if (entity is null)
            throw new KeyNotFoundException($"Language with id {id} not found");

        _mapper.Map(dto, entity);

        await _repository.UpdateItemAsync(entity, cancellationToken);

        return _mapper.Map<ResponseLanguageDto>(entity);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetItemByIdAsync(id, cancellationToken);
        if (entity is null)
            throw new KeyNotFoundException($"Language with id {id} not found");

        await _repository.DeleteItemAsync(entity, cancellationToken);
    }
}
