namespace MainService.DAL;

public interface IEntity<TKey>
{
    public TKey Id { get; set; }
}