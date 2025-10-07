using MainService.AL.Features.Translations.DTO.Request;
using MainService.AL.Features.Translations.DTO.Response;
using MainService.BLL.Data.Translations.Repository;
using MainService.DAL.Abstractions;
using MainService.DAL.Features.Translations.Models;
using Mapster;
using MapsterMapper;

namespace MainService.AL.Features.Translations.Services;

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

    public async Task<PaginatedList<ResponseTranslationDto>> GetAllAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var  entities = await _repository.GetAllItemsAsync(pageIndex, pageSize, cancellationToken);
        var list = entities.Items.Adapt<List<ResponseTranslationDto>>();
        return new PaginatedList<ResponseTranslationDto>(list, pageIndex, pageSize);
    }

    public async Task<ResponseTranslationDto> CreateAsync(RequestTranslationDto dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Translation>(dto);
        entity.Id = Guid.NewGuid();
        await _repository.AddItemAsync(entity, cancellationToken);
        return _mapper.Map<ResponseTranslationDto>(entity);
    }

    public async Task<ResponseTranslationDto> UpdateAsync(Guid id, RequestTranslationDto dto, CancellationToken cancellationToken)
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
