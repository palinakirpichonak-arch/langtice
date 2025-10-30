using AuthService.DAL.Abstractions;
using AuthService.DAL.Features.Roles.Models;
using Dapper;

namespace AuthService.DAL.Features.Roles.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly IDapperDbConnection _db;
    
    public RoleRepository(IDapperDbConnection db) => _db = db;
    
    public async Task<IReadOnlyList<string>> GetUserRolesAsync(Guid userId, CancellationToken cancellationToken)
    {
       const string query = @"
                    SELECT roles.name
                    FROM auth.user_roles user_roles
                    JOIN auth.roles roles ON roles.id = user_roles.role_id
                    WHERE user_roles.user_id = @userId;";
       
       await using var conn = await _db.CreateOpenConnectionAsync(cancellationToken);
       
       var roles = await conn.QueryAsync<string>(query, new { UserId = userId });
       return roles.ToArray();
    }

    public async Task AssignUserRolesAsync(Guid userId, string roleName, CancellationToken cancellationToken)
    {
        const string query = @"
                    INSERT INTO auth.user_roles(user_id, role_id)
                    SELECT @UserId, r.id FROM auth.roles r WHERE r.name = @RoleName;";
        
        await using var conn = await _db.CreateOpenConnectionAsync(cancellationToken);
        await conn.ExecuteAsync(query, new { UserId = userId, RoleName = roleName });
    }
}