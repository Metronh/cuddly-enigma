

namespace UserService.Models.Response;

public class LoginResponse : BaseResponse
{
    public bool Success { get; set; }
    public string? Token { get; set; }
};