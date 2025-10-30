using AuthService.DAL.Features.Roles.Models;

namespace AuthService.DAL.Features.Roles.Repositories;

public interface IRoleRepository
{
    Task<IReadOnlyList<string>> GetUserRolesAsync(Guid userId, CancellationToken cancellationToken);
    Task AssignUserRolesAsync(Guid userId, string roleName, CancellationToken cancellationToken);
}