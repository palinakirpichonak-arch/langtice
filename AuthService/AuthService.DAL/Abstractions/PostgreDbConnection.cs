using System.Data;
using Npgsql;

namespace AuthService.DAL.Abstractions;

public class PostgreDbConnection :  IDapperDbConnection
{
    private readonly string _connectionString;

    public PostgreDbConnection(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public IDbConnection CreateConnection()
    {
       return new NpgsqlConnection(_connectionString);
    }
    
}
