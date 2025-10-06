using MainService.DAL.Abstractions;

namespace MainService.DAL.Features.Languages.Models;

public class Language : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!; 
}