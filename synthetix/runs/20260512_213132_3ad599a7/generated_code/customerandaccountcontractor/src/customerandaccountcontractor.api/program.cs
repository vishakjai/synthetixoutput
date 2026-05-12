using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/health", () => Results.Ok(new { status = "healthy" }));
app.MapGet("/ready", () => Results.Ok(new { status = "ready" }));

app.MapGet("/contractorplacement/contractorplacemententitycontroller", () => Results.Ok(new { status = "success" }));
app.MapGet("/contractorplacement/contractorplacementsearchcontroller", () => Results.Ok(new { status = "success" }));
app.MapGet("/customer/profile", () => Results.Ok(new { status = "success" }));
app.MapGet("/customer/accountlistentitycontroller", () => Results.Ok(new { status = "success" }));
app.MapGet("/customer/accountlistsearchcontroller", () => Results.Ok(new { status = "success" }));
app.MapGet("/customer/accountsearchcontroller", () => Results.Ok(new { status = "success" }));
app.MapGet("/customer/contactsearchcontroller", () => Results.Ok(new { status = "success" }));
app.MapGet("/customers/{customerId}", (int customerId) => Results.Ok(new { status = "success" }));
app.MapGet("/customer/joblistingsearchcontroller", () => Results.Ok(new { status = "success" }));
app.MapGet("/customer/trainingsearchcontroller", () => Results.Ok(new { status = "success" }));
app.MapGet("/customer/vmsportalsearchcontroller", () => Results.Ok(new { status = "success" }));
app.MapGet("/onboarding/apicontroller", () => Results.Ok(new { status = "success" }));
app.MapGet("/onboarding/onbdemographicsearchcontroller", () => Results.Ok(new { status = "success" }));
app.MapGet("/platform/accountentitycontroller", () => Results.Ok(new { status = "success" }));
app.MapPost("/platform/addressentitycontroller", (int customerId, string accountNo, object payload) => Results.Ok(new { status = "success", entity_id = 1 }));
app.MapGet("/platform/crosssellingleadsearchcontroller", () => Results.Ok(new { status = "success" }));
app.MapGet("/platform/endcliententitycontroller", () => Results.Ok(new { status = "success" }));
app.MapGet("/platform/searchcontroller", () => Results.Ok(new { status = "success" }));
app.MapGet("/platform/tsoverdueentitycontroller", () => Results.Ok(new { status = "success" }));
app.MapGet("/reporting/ondemandtimesheetssearchcontroller", (string accountNo, DateTime fromDate, DateTime toDate) => Results.Ok(new { items = new object[0], generated_at = DateTime.UtcNow }));
app.MapGet("/reporting/tickersearchcontroller", (string accountNo, DateTime fromDate, DateTime toDate) => Results.Ok(new { items = new object[0], generated_at = DateTime.UtcNow }));
app.MapGet("/reporting/tsdailysearchcontroller", (string accountNo, DateTime fromDate, DateTime toDate) => Results.Ok(new { items = new object[0], generated_at = DateTime.UtcNow }));
app.MapGet("/reporting/tsondemandtransferdetailssearchcontroller", (string accountNo, DateTime fromDate, DateTime toDate) => Results.Ok(new { items = new object[0], generated_at = DateTime.UtcNow }));
app.MapPut("/reporting/tsupdatesearchcontroller", (int customerId, string accountNo, object payload) => Results.Ok(new { status = "success", entity_id = 1 }));
app.MapPost("/sharedutilities/addresssearchcontroller", (int customerId, string accountNo, object payload) => Results.Ok(new { status = "success", entity_id = 1 }));
app.MapPost("/sharedutilities/addrmasterentitycontroller", (int customerId, string accountNo, object payload) => Results.Ok(new { status = "success", entity_id = 1 }));
app.MapPost("/sharedutilities/addrmastersearchcontroller", (int customerId, string accountNo, object payload) => Results.Ok(new { status = "success", entity_id = 1 }));
app.MapGet("/sharedutilities/archcontractorentitycontroller", () => Results.Ok(new { status = "success" }));
app.MapGet("/sharedutilities/archcontractorsearchcontroller", () => Results.Ok(new { status = "success" }));
app.MapGet("/sharedutilities/assetsearchcontroller", () => Results.Ok(new { status = "success" }));
app.MapGet("/sharedutilities/auditorsearchcontroller", () => Results.Ok(new { status = "success" }));
app.MapGet("/sharedutilities/backuphrasearchcontroller", () => Results.Ok(new { status = "success" }));

app.Run();

public partial class Program { }