namespace UserService.Models.Response;

public class ValidationFailureResponse
{
    public required string Property { get; set; }
    public required string ErrorMessage { get; set; }
}