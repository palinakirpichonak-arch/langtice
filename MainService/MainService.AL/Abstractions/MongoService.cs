using MainService.AL.Mappers;
using MainService.DAL.Abstractions;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MainService.AL.Abstractions;

public abstract class MongoService<T, TDto> : IMongoService<T, TDto>
    where T : class
    where TDto : IMapper<T>
{
    protected readonly IMongoRepository<T> _repository;

    public MongoService(IMongoRepository<T> repository)
    {
        _repository = repository;
    }

    public async Task<T?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync(cancellationToken);
    }

    public async Task<T> CreateAsync(TDto dto, CancellationToken cancellationToken)
    {
        var entity = dto.ToEntity();
        await _repository.AddAsync(entity, cancellationToken);
        return entity;
    }

    public async Task<T> UpdateAsync(string id, TDto dto, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        if (entity == null)
            throw new KeyNotFoundException($"Entity with id {id} not found");

        dto.MapTo(entity);
        await _repository.UpdateAsync(entity, cancellationToken);
        return entity;
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        if (entity != null)
        {
            await _repository.DeleteAsync(id, cancellationToken);
        }
    }
}