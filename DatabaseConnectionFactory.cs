using System.Data;
using Oracle.ManagedDataAccess.Client;
namespace TaskManager.API.Data;
public class DatabaseConnectionFactory
{
    private readonly string _connectionString;
    public DatabaseConnectionFactory(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("OracleDb")!;
    }
    public IDbConnection CreateConnection() => new OracleConnection(_connectionString);
}
