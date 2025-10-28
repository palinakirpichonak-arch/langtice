using System.Data;
using AuthService.DAL.Users;
using Dapper;
using MainService.DAL.Abstractions;

namespace AuthService.DAL.Abstractions;

public class UserRepository : IUserRepository
{
    private readonly IDapperDbConnection _dbConnection;

    public UserRepository(IDapperDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }
    
    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        using IDbConnection db = _dbConnection.CreateConnection();
        
        string query = "SELECT * FROM Users WHERE Id = @Id";
        
        return await db.QueryFirstOrDefaultAsync(query, new { Id = id });
    }
    
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        using IDbConnection db = _dbConnection.CreateConnection();
        
        string query = "SELECT * FROM Users WHERE Email = @Email";
        
        return await db.QueryFirstOrDefaultAsync(query, new { Email = email });
    }

    public async Task AddAsync(User newUser, CancellationToken cancellationToken)
    {
        using IDbConnection db = _dbConnection.CreateConnection();
        string query = "INSERT INTO Users(Id, Username, Email, PasswordHash, AvatarUrl,Status)" +
                       "VALUES(@Id, @Username, @Email, @PasswordHash, @AvatarUrl, TRUE)";
        
        await db.ExecuteScalarAsync(query, newUser);
    }

    public async Task UpdateAsync(User updatedUser, CancellationToken cancellationToken)
    {
        using IDbConnection db = _dbConnection.CreateConnection();
        string query = "UPDATE Users SET Name = @Name WHERE Id = @Id";
        
        await db.ExecuteAsync(query, new { Id = updatedUser.Id });
    }
}