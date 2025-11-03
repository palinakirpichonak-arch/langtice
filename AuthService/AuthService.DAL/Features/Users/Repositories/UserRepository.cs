using AuthService.DAL.Abstractions;
using AuthService.DAL.Features.Users.Models;
using Dapper;

namespace AuthService.DAL.Features.Users.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IDapperDbConnection _db;

    public UserRepository(IDapperDbConnection db) => _db = db;

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        const string sql = @"
            SELECT id, username, email, password_hash, avatar_url, status, created_at
            FROM auth.users
            WHERE id = @Id
            LIMIT 1;";

        await using var conn = await _db.CreateOpenConnectionAsync(ct);
        return await conn.QueryFirstOrDefaultAsync<User>(
            new CommandDefinition(sql, new { Id = id }, cancellationToken: ct));
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken ct)
    {
        const string sql = @"
            SELECT id, username, email, password_hash, avatar_url, status, created_at
            FROM auth.users
            WHERE email = @Email
            LIMIT 1;";

        await using var conn = await _db.CreateOpenConnectionAsync(ct);
        return await conn.QueryFirstOrDefaultAsync<User>(
            new CommandDefinition(sql, new { Email = email }, cancellationToken: ct));
    }

    public async Task AddAsync(User newUser, CancellationToken ct)
    {
        const string sql = @"
            INSERT INTO auth.users (id, username, email, password_hash, avatar_url, status, created_at)
            VALUES (@Id, @Username, @Email, @PasswordHash, @AvatarUrl, @Status, now());";

        await using var conn = await _db.CreateOpenConnectionAsync(ct);
        await conn.ExecuteAsync(new CommandDefinition(sql, new {
            newUser.Id,
            newUser.Username,
            newUser.Email,
            newUser.PasswordHash,
            newUser.AvatarUrl,
            newUser.Status
        }, cancellationToken: ct));
    }
    
    public async Task UpdateAsync(User u, CancellationToken ct)
    {
        const string sql = @"
            UPDATE auth.users
            SET username = @Username,
                email = @Email,
                password_hash = @PasswordHash,
                avatar_url = @AvatarUrl,
                status = @Status
            WHERE id = @Id;";

        await using var conn = await _db.CreateOpenConnectionAsync(ct);
        await conn.ExecuteAsync(new CommandDefinition(sql, new {
            u.Id,
            u.Username,
            u.Email,
            u.PasswordHash,
            u.AvatarUrl,
            u.Status
        }, cancellationToken: ct));
    }
}
