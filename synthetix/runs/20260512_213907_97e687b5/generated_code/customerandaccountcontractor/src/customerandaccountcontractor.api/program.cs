using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/health", () => Results.Ok(new { status = "healthy" }));
app.MapGet("/ready", () => Results.Ok(new { status = "ready" }));

// Placeholder for additional endpoints
// app.MapGet("/contractorplacement/contractorplacemententitycontroller", () => Results.Ok(new { status = "success" }));

app.Run();

public partial class Program { }