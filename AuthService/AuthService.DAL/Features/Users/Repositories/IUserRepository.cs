using AuthService.DAL.Features.Users.Models;

namespace AuthService.DAL.Features.Users.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task AddAsync(User newUser, CancellationToken cancellationToken);
    Task UpdateAsync(User updatedUser, CancellationToken cancellationToken);
}