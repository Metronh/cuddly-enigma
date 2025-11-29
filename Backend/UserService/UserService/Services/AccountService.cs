using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using UserService.Entities;
using UserService.Interfaces.Repository;
using UserService.Interfaces.Services;
using UserService.Models;
using UserService.Models.Response;
using UserService.Validation;

namespace UserService.Services;

public class AccountService : IAccountService
{
    private readonly ILogger<AccountService> _logger;
    private readonly CreateUserRequestValidator _createUserRequestValidator;
    private readonly IUserRepository _userRepository;
    private readonly PasswordHasher<UserEntity> _hasher;

    public AccountService(ILogger<AccountService> logger, CreateUserRequestValidator createUserRequestValidator,
        IUserRepository userRepository, PasswordHasher<UserEntity> hasher)
    {
        _logger = logger;
        _createUserRequestValidator = createUserRequestValidator;
        _userRepository = userRepository;
        _hasher = hasher;
    }

    public async Task<CreateUserResponse> CreateUser(CreateUserRequest request, bool isAdministrator = false)
    {
        _logger.LogInformation("{Class}.{Method} started at {Time}",
            nameof(AccountService), nameof(CreateUser), DateTime.UtcNow);
        var response = new CreateUserResponse
        {
            IsAccountCreated = false,
            ValidationFailures = new List<ValidationFailureResponse>(),
        };
        ValidationResult validationResult = await _createUserRequestValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Account {request} not created because account info not valid",
                request.Username);
            response.ValidationFailures = validationResult.Errors.Select(e => new ValidationFailureResponse
            {
                Property = e.PropertyName,
                ErrorMessage = e.ErrorMessage,
            }).ToList();
            return response;
        }

        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Username = request.Username,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Password = request.Password,
            Administrator = isAdministrator,
            CreatedAt = DateOnly.FromDateTime(DateTime.Now),
        };

        user.Password = _hasher.HashPassword(user, user.Password);
        await _userRepository.CreateUser(user: user);

        _logger.LogInformation("{Class}.{Method} completed at {Time}",
            nameof(AccountService), nameof(CreateUser), DateTime.UtcNow);
        return response;
    }
}