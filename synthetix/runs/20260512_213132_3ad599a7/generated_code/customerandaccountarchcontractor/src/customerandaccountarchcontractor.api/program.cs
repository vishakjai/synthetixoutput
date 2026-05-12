using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/health", () => Results.Ok(new { status = "healthy" }));
app.MapGet("/ready", () => Results.Ok(new { status = "ready" }));

// Example endpoint
app.MapGet("/contractorplacement/contractorplacemententitycontroller", () => Results.Ok(new { status = "success" }));

app.Run();

public partial class Program { }