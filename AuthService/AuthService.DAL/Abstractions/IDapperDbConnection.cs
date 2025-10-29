using Npgsql;

namespace AuthService.DAL.Abstractions;

public interface IDapperDbConnection
{
    public Task<NpgsqlConnection> CreateOpenConnectionAsync(CancellationToken cancellationToken);
}