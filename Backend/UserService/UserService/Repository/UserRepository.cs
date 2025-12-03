using Dapper;
using UserService.Entities;
using UserService.Interfaces.Database;
using UserService.Interfaces.Repository;

namespace UserService.Repository;

public class UserRepository : IUserRepository
{
    private readonly IDbConnectionFactory _connectionFactory;
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(IDbConnectionFactory connectionFactory, ILogger<UserRepository> logger)
    {
        _connectionFactory = connectionFactory;
        _logger = logger;
    }

    public async Task CreateUser(UserEntity user)
    {
        _logger.LogInformation("{Class}.{Method} started at {Time}",
            nameof(UserRepository), nameof(CreateUser), DateTime.UtcNow);
        using var connection = await _connectionFactory.CreateConnectionAsync();
        await connection.ExecuteAsync(
            """
            INSERT INTO users (Id, Username, Email, FirstName, LastName, Administrator, Password) 
            VALUES 
                (@Id, @Username, @Email, @FirstName, @LastName, @Administrator, @Password)
            """, user
        );

        _logger.LogInformation("{Class}.{Method} completed at {Time}",
            nameof(UserRepository), nameof(CreateUser), DateTime.UtcNow);
    }

    public async Task<UserEntity?> GetUserLoginDetails(string username)
    {
        _logger.LogInformation("{Class}.{Method} started at {Time}",
            nameof(UserRepository), nameof(GetUserLoginDetails), DateTime.UtcNow);

        using var connection = await _connectionFactory.CreateConnectionAsync();
        
        
        var user = await connection.QueryFirstOrDefaultAsync<UserEntity>("""
                                                                SELECT * FROM users WHERE username = @Username
                                                                """, new {Username = username});
        
        
        _logger.LogInformation("{Class}.{Method} completed at {Time}",
            nameof(UserRepository), nameof(GetUserLoginDetails), DateTime.UtcNow);
        return user;
    }
}