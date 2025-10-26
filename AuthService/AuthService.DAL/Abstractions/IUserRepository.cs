using AuthService.DAL.Users;

namespace AuthService.DAL.Abstractions;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task CreateAsync(User newUser, CancellationToken cancellationToken);
    Task UpdateAsync(User updatedUser, CancellationToken cancellationToken);
}