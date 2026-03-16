using System.Text;
using System.Text.RegularExpressions;
using AuthenticationService.Data;
using AuthenticationService.Models;
using AuthenticationService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var tokenOptions = new AuthTokenOptions
{
    Issuer = builder.Configuration["AUTH_TOKEN_ISSUER"] ?? "synthetix-auth",
    Audience = builder.Configuration["AUTH_TOKEN_AUDIENCE"] ?? "modernized-app",
    SigningKey = builder.Configuration["AUTH_TOKEN_SIGNING_KEY"] ?? throw new InvalidOperationException("AUTH_TOKEN_SIGNING_KEY is required."),
    ExpiryMinutes = int.TryParse(builder.Configuration["AUTH_TOKEN_EXPIRY_MINUTES"], out var expiryMinutes) ? expiryMinutes : 30,
};
if (tokenOptions.SigningKey.Length < 32)
{
    throw new InvalidOperationException("AUTH_TOKEN_SIGNING_KEY must be at least 32 characters.");
}

var lockoutOptions = new AuthLockoutOptions
{
    FailureThreshold = int.TryParse(builder.Configuration["AUTH_LOCKOUT_THRESHOLD"], out var threshold) ? threshold : 5,
};

var repoOptions = new PostgresCredentialRepositoryOptions
{
    TableName = ResolveTableName(builder.Configuration["AUTH_DB_TABLE"]),
};

builder.Services.AddSingleton(tokenOptions);
builder.Services.AddSingleton(lockoutOptions);
builder.Services.AddSingleton(repoOptions);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidateAudience = true,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SigningKey)),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(1),
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddSingleton<ITokenIssuer, JwtTokenIssuer>();
builder.Services.AddSingleton<IPasswordHashVerifier, PasswordHashVerifier>();
builder.Services.AddScoped<INpgsqlConnectionFactory, NpgsqlConnectionFactory>();
builder.Services.AddScoped<IUserCredentialRepository, PostgresUserCredentialRepository>();
builder.Services.AddScoped<ILoginLockoutPolicy, DatabaseLoginLockoutPolicy>();
builder.Services.AddScoped<IAuthenticationService, CredentialAuthenticationService>();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program;

static string ResolveTableName(string? configuredName)
{
    var candidate = string.IsNullOrWhiteSpace(configuredName) ? "user_credentials" : configuredName.Trim();
    if (!Regex.IsMatch(candidate, "^[A-Za-z_][A-Za-z0-9_]*$"))
    {
        throw new InvalidOperationException("AUTH_DB_TABLE contains unsupported characters.");
    }

    return candidate;
}
