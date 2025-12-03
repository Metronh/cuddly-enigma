using Microsoft.AspNetCore.Identity;
using UserService.AppSettings;
using UserService.Database;
using UserService.Entities;
using UserService.Interfaces.Database;
using UserService.Interfaces.Repository;
using UserService.Interfaces.Services;
using UserService.Repository;
using UserService.Services;
using UserService.Validation;

namespace UserService.SetUpProject;

public static class SetUp
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