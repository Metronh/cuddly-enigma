using UserService.Middleware;

namespace UserService.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static void UseCustomExceptionHandling(this WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}