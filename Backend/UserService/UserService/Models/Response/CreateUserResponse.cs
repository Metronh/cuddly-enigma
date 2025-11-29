using FluentValidation.Results;

namespace UserService.Models.Response;

public class CreateUserResponse
{
    public bool IsAccountCreated { get; set; }
    public List<ValidationFailureResponse> ValidationFailures { get; set; }
}

public class ValidationFailureResponse
{
    public required string Property { get; set; }
    public required string ErrorMessage { get; set; }
}