using MainService.AL.Features.Words.DTO.Request;
using MainService.AL.Features.Words.DTO.Response;
using MainService.BLL.Services;
using MainService.DAL.Abstractions;
using MainService.DAL.Features.Words.Models;
using Mapster;
using MapsterMapper;

namespace MainService.AL.Features.Words.Services;

public class WordService : IWordService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public WordService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseWordDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Words.GetItemByIdAsync(id, cancellationToken);
        return entity is null ? null : _mapper.Map<ResponseWordDto>(entity);
    }

    public async Task<PaginatedList<ResponseWordDto>> GetAllAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.Words.GetAllItemsAsync(pageIndex, pageSize, cancellationToken);
        var list = entities.Items.Adapt<List<ResponseWordDto>>();
        return new PaginatedList<ResponseWordDto>(list, pageIndex, pageSize);
    }

    public async Task<ResponseWordDto> CreateAsync(RequestWordDto dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Word>(dto);
        entity.Id = Guid.NewGuid();

        _unitOfWork.Words.AddItem(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ResponseWordDto>(entity);
    }

    public async Task<ResponseWordDto> UpdateAsync(Guid id, RequestWordDto dto, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Words.GetItemByIdAsync(id, cancellationToken);
        if (entity is null) throw new KeyNotFoundException($"Word {id} not found");

        _mapper.Map(dto, entity);
        _unitOfWork.Words.UpdateItem(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ResponseWordDto>(entity);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Words.GetItemByIdAsync(id, cancellationToken);
        if (entity is null) throw new KeyNotFoundException($"Word {id} not found");

        _unitOfWork.Words.DeleteItem(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
