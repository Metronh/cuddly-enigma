using UserService.Entities;

namespace UserService.Interfaces.Repository;

public interface IUserRepository
{
    public Task CreateUser(UserEntity user);
}