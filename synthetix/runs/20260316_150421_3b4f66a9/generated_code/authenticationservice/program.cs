using AuthenticationService.Data;
using AuthenticationService.Models;
using AuthenticationService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton(new AuthTokenOptions
{
    Issuer = builder.Configuration["AUTH_TOKEN_ISSUER"] ?? "synthetix-auth",
    Audience = builder.Configuration["AUTH_TOKEN_AUDIENCE"] ?? "modernized-app",
    SigningKey = builder.Configuration["AUTH_TOKEN_SIGNING_KEY"] ?? throw new InvalidOperationException("AUTH_TOKEN_SIGNING_KEY is required."),
    ExpiryMinutes = int.TryParse(builder.Configuration["AUTH_TOKEN_EXPIRY_MINUTES"], out var expiryMinutes) ? expiryMinutes : 30,
});
builder.Services.AddSingleton<ITokenIssuer, HmacTokenIssuer>();
builder.Services.AddSingleton<IPasswordHashVerifier, PasswordHashVerifier>();
builder.Services.AddScoped<INpgsqlConnectionFactory, NpgsqlConnectionFactory>();
builder.Services.AddScoped<IUserCredentialRepository, PostgresUserCredentialRepository>();
builder.Services.AddScoped<IAuthenticationService, CredentialAuthenticationService>();

var app = builder.Build();
app.MapControllers();
app.Run();

public partial class Program;
