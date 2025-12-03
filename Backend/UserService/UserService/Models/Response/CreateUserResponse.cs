using FluentValidation.Results;

namespace UserService.Models.Response;

public class CreateUserResponse : BaseResponse
{
    public bool IsAccountCreated { get; set; }
}