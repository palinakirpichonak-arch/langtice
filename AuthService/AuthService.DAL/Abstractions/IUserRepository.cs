using AuthService.DAL.Users;

namespace AuthService.DAL.Abstractions;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task AddAsync(User newUser, CancellationToken cancellationToken);
    Task UpdateAsync(User updatedUser, CancellationToken cancellationToken);
}