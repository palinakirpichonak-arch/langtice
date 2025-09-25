namespace MainService.AL.Mappers;

public interface IMapper<TEntity>
{
    TEntity ToEntity();       // Convert DTO to new entity
    void MapTo(TEntity entity); // Map DTO properties to existing entity
}