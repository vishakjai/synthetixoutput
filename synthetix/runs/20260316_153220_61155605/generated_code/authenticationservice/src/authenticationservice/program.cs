using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using AuthenticationService.Data;
using AuthenticationService.Services;
using AuthenticationService.Validation;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Bind port from PORT env (default 8080)
var portEnv = Environment.GetEnvironmentVariable("PORT");
var port = 8080;
if (!string.IsNullOrWhiteSpace(portEnv) && int.TryParse(portEnv, out var parsed))
{
    port = parsed;
}

// JSON options
builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

// Validators
builder.Services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();

// OpenTelemetry
var serviceName = "AuthenticationService";
var resourceBuilder = ResourceBuilder.CreateDefault().AddService(serviceName: serviceName);
builder.Services.AddOpenTelemetry()
    .ConfigureResource(rb => rb.AddService(serviceName))
    .WithMetrics(mb =>
    {
        mb.AddAspNetCoreInstrumentation();
        mb.AddHttpClientInstrumentation();
        mb.AddPrometheusExporter();
    })
    .WithTracing(tb =>
    {
        tb.AddAspNetCoreInstrumentation();
        tb.AddHttpClientInstrumentation();
    });

// Data layer
builder.Services.AddSingleton<IConnectionFactory, NpgsqlConnectionFactory>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Domain services
builder.Services.AddScoped<IAuthenticationService, JwtAuthenticationService>();

// JWT authentication setup - uses env-provided secret
var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET");
if (string.IsNullOrWhiteSpace(jwtSecret))
{
    throw new InvalidOperationException("JWT_SECRET must be provided via environment for secure token generation.");
}
var key = Encoding.UTF8.GetBytes(jwtSecret);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapPrometheusScrapingEndpoint();

app.Urls.Clear();
app.Urls.Add($"http://0.0.0.0:{port}");

app.Run();
