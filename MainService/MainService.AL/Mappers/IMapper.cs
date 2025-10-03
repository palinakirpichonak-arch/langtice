namespace MainService.AL.Mappers;

public interface IMapper<TEntity>
{
    TEntity ToEntity();     
    void ToDto(TEntity entity); 
}