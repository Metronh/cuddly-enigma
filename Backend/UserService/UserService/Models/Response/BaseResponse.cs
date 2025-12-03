namespace UserService.Models.Response;

public class BaseResponse
{
    public List<ValidationFailureResponse>? ValidationFailures { get; set; }
}