using Npgsql;

namespace AuthService.DAL.Abstractions;

public class PostgreDbConnection :  IDapperDbConnection
{
    private readonly string _connectionString;
    
    public PostgreDbConnection(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public async Task<NpgsqlConnection> CreateOpenConnectionAsync(CancellationToken cancellationToken)
    {
       var connection = new NpgsqlConnection(_connectionString);
       
       await connection.OpenAsync(cancellationToken);
       return connection;
    }
    
}
