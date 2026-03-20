using CustomerService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/health", () => Results.Json(new { status = "healthy" }));
app.MapGet("/ready", () => Results.Json(new { status = "ready" }));
app.MapRazorPages();
app.MapControllers();

app.Run();
