using System.Data;

namespace UserService.Interfaces.Database;

public interface IDbConnectionFactory
{
    public Task<IDbConnection> CreateConnectionAsync(CancellationToken token = default);
}