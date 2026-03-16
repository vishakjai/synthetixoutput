using AuthenticationService.Data;
using AuthenticationService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddDbContext<CredentialStoreContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IAuthenticationService, AuthenticationService.Services.AuthenticationService>();
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddControllers();

var app = builder.Build();

// Configure middleware
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();