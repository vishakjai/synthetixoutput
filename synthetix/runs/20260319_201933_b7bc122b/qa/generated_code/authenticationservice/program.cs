var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddSingleton<ScreenValidationService>();
builder.Services.AddSingleton<UiEventExecutionService>();
builder.Services.AddSingleton<UiEventRegistry>();

var app = builder.Build();

app.MapGet("/health", () => Results.Json(new { status = "healthy" }));
app.MapGet("/ready", () => Results.Json(new { status = "ready" }));
app.MapRazorPages();
app.MapControllers();

app.Run();
