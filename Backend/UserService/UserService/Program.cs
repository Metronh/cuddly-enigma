using Microsoft.AspNetCore.Identity;
using UserService.AppSettings;
using UserService.Database;
using UserService.Endpoints;
using UserService.Entities;
using UserService.Extensions;
using UserService.Interfaces.Database;
using UserService.Interfaces.Repository;
using UserService.Interfaces.Services;
using UserService.Repository;
using UserService.Services;
using UserService.SetUpProject;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.AddSwagger();
builder.AddAppSettings();
builder.AddValidators();
builder.AddServices();

builder.Services.AddSingleton<IDbConnectionFactory, NpgsqlDbConnectionFactory>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<PasswordHasher<UserEntity>>();


var app = builder.Build();
app.UseCustomExceptionHandling();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.AddHealthEndpoints();
app.AddUserEndpoints();
app.Run();