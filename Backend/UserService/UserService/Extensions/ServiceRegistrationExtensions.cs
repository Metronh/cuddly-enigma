using UserService.AppSettings;
using UserService.Interfaces.Services;
using UserService.Services;
using UserService.Validation;

namespace UserService.Extensions;

public static class ServiceRegistrationExtensions
{
    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAccountService, AccountService>();
        builder.Services.AddSingleton<ITokenService, TokenService>();
    }

    public static void AddValidators(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<CreateUserRequestValidator>();
        builder.Services.AddScoped<LoginRequestValidator>();
    }

    public static void AddSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }

    public static void AddAppSettings(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));
        builder.Services.Configure<JwtInformation>(builder.Configuration.GetSection("JwtTokenInformation"));
    }
}