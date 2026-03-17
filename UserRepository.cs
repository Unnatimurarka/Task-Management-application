using Dapper;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;
namespace TaskManager.API.Data;
public class UserRepository : IUserRepository
{
    private readonly DatabaseConnectionFactory _db;
    public UserRepository(DatabaseConnectionFactory db) => _db = db;

    public async Task<User?> GetByUsernameAsync(string username)
    {
        using var conn = _db.CreateConnection();
        return await conn.QuerySingleOrDefaultAsync<User>(
            "SELECT id, username, password_hash as PasswordHash, created_at as CreatedAt FROM USERS WHERE username = :Username", 
            new { Username = username });
    }

    public async Task<int> CreateAsync(User user)
    {
        using var conn = _db.CreateConnection();
        var p = new Oracle.ManagedDataAccess.Client.OracleDynamicParameters();
        p.Add("username", user.Username);
        p.Add("password_hash", user.PasswordHash);
        p.Add("id", dbType: Oracle.ManagedDataAccess.Client.OracleDbType.Decimal, direction: System.Data.ParameterDirection.ReturnValue);
        
        await conn.ExecuteAsync(
            "INSERT INTO USERS (username, password_hash) VALUES (:username, :password_hash) RETURNING id INTO :id", p);
        
        return Convert.ToInt32(p.Get<decimal>("id"));
    }
}
