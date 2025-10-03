using MainService.DAL.Abstractions;
using MainService.DAL.Features.Courses.Models;
using MainService.DAL.Features.Words.Models;
using MainService.DAL.Models;

namespace MainService.DAL.Features.Languages.Models;

public class Language : IEntity<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!; 
}