namespace MainService.DAL.Abstractions;

public interface IEntity<TKey>
{
    public TKey Id { get; set; }
}