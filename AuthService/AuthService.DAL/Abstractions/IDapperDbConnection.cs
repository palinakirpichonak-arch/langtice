using System.Data;

namespace AuthService.DAL.Abstractions;

public interface IDapperDbConnection
{
    public IDbConnection CreateConnection();
}