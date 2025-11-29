using UserService.Models;
using UserService.Models.Response;

namespace UserService.Interfaces.Services;

public interface IAccountService
{
    public Task<CreateUserResponse> CreateUser(CreateUserRequest request, bool isAdministrator = false);
}