using Npgsql;

namespace AuthService.DAL.Abstractions;

public class PostgreDbConnection :  IDapperDbConnection
{
    private readonly string _connectionString;
    private static bool _isConfigured;
    
    public PostgreDbConnection(string connectionString)
    {
        _connectionString = connectionString;

        if (!_isConfigured)
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            _isConfigured = true;
        }
    }
    
    public async Task<NpgsqlConnection> CreateOpenConnectionAsync(CancellationToken cancellationToken)
    {
       var connection = new NpgsqlConnection(_connectionString);
       
       await connection.OpenAsync(cancellationToken);
       return connection;
    }
    
}
