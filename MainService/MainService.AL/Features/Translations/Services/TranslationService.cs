using MainService.AL.Features.Translations.DTO;
using MainService.AL.Features.Translations.DTO.Response;
using MainService.AL.Features.Translations.Services;
using MainService.BLL.Data.Translations.Repository;
using MainService.DAL.Features.Translations.Models;
using MapsterMapper;

namespace MainService.IL.Translations.Services;

public class TranslationService : ITranslationService
{
    private readonly ITranslationRepository _repository;
    private readonly IMapper _mapper;

    public TranslationService(ITranslationRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseTranslationDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetItemByIdAsync(id, cancellationToken);
        return entity is null ? null : _mapper.Map<ResponseTranslationDto>(entity);
    }

    public async Task<IEnumerable<ResponseTranslationDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllItemsAsync(cancellationToken);
        return _mapper.Map<IEnumerable<ResponseTranslationDto>>(entities);
    }

    public async Task<ResponseTranslationDto> CreateAsync(TranslationDto dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Translation>(dto);
        entity.Id = Guid.NewGuid();
        await _repository.AddItemAsync(entity, cancellationToken);
        return _mapper.Map<ResponseTranslationDto>(entity);
    }

    public async Task<ResponseTranslationDto> UpdateAsync(Guid id, TranslationDto dto, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetItemByIdAsync(id, cancellationToken);
        if (entity is null) throw new KeyNotFoundException($"Translation {id} not found");

        _mapper.Map(dto, entity);
        await _repository.UpdateItemAsync(entity, cancellationToken);
        return _mapper.Map<ResponseTranslationDto>(entity);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetItemByIdAsync(id, cancellationToken);
        if (entity is null) throw new KeyNotFoundException($"Translation {id} not found");

        await _repository.DeleteItemAsync(entity, cancellationToken);
    }
}
