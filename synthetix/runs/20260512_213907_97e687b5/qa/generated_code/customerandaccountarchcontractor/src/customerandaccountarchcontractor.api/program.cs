using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/health", () => Results.Ok(new { status = "healthy" }));
app.MapGet("/ready", () => Results.Ok(new { status = "ready" }));

app.MapGet("/contractorplacement/contractorplacemententitycontroller", () => Results.Ok(new { status = "success" }));
app.MapGet("/contractorplacement/contractorplacementsearchcontroller", () => Results.Ok(new { status = "success" }));
app.MapGet("/customer/profile", () => Results.Ok(new { status = "success" }));
app.MapGet("/customer/accountlistentitycontroller", () => Results.Ok(new { status = "success" }));
app.MapGet("/customer/accountlistsearchcontroller", () => Results.Ok(new { status = "success" }));
app.MapGet("/customer/accountsearchcontroller", () => Results.Ok(new { status = "success" }));
app.MapGet("/customer/contactsearchcontroller", () => Results.Ok(new { status = "success" }));
app.MapGet("/customers/{customerId}", (string customerId) => Results.Ok(new { status = "success", customerId }));
app.MapGet("/customer/joblistingsearchcontroller", () => Results.Ok(new { status = "success" }));
app.MapGet("/customer/trainingsearchcontroller", () => Results.Ok(new { status = "success" }));
app.MapGet("/customer/vmsportalsearchcontroller", () => Results.Ok(new { status = "success" }));
app.MapGet("/onboarding/apicontroller", () => Results.Ok(new { status = "success" }));
app.MapGet("/onboarding/onbdemographicsearchcontroller", () => Results.Ok(new { status = "success" }));
app.MapGet("/platform/accountentitycontroller", () => Results.Ok(new { status = "success" }));
app.MapPost("/platform/addressentitycontroller", (dynamic payload) => Results.Ok(new { status = "success", entity_id = 1 }));
app.MapGet("/platform/crosssellingleadsearchcontroller", () => Results.Ok(new { status = "success" }));
app.MapGet("/platform/endcliententitycontroller", () => Results.Ok(new { status = "success" }));
app.MapGet("/platform/searchcontroller", () => Results.Ok(new { status = "success" }));
app.MapGet("/platform/tsoverdueentitycontroller", () => Results.Ok(new { status = "success" }));
app.MapGet("/reporting/ondemandtimesheetssearchcontroller", (string account_no, string from_date, string to_date) => Results.Ok(new { items = new object[0], generated_at = DateTime.UtcNow }));
app.MapGet("/reporting/tickersearchcontroller", (string account_no, string from_date, string to_date) => Results.Ok(new { items = new object[0], generated_at = DateTime.UtcNow }));
app.MapGet("/reporting/tsdailysearchcontroller", (string account_no, string from_date, string to_date) => Results.Ok(new { items = new object[0], generated_at = DateTime.UtcNow }));
app.MapGet("/reporting/tsondemandtransferdetailssearchcontroller", (string account_no, string from_date, string to_date) => Results.Ok(new { items = new object[0], generated_at = DateTime.UtcNow }));
app.MapPut("/reporting/tsupdatesearchcontroller", (dynamic payload) => Results.Ok(new { status = "success", entity_id = 1 }));
app.MapPost("/sharedutilities/addresssearchcontroller", (dynamic payload) => Results.Ok(new { status = "success", entity_id = 1 }));
app.MapPost("/sharedutilities/addrmasterentitycontroller", (dynamic payload) => Results.Ok(new { status = "success", entity_id = 1 }));
app.MapPost("/sharedutilities/addrmastersearchcontroller", (dynamic payload) => Results.Ok(new { status = "success", entity_id = 1 }));
app.MapGet("/sharedutilities/archcontractorentitycontroller", () => Results.Ok(new { status = "success" }));
app.MapGet("/sharedutilities/archcontractorsearchcontroller", () => Results.Ok(new { status = "success" }));
app.MapGet("/sharedutilities/assetsearchcontroller", () => Results.Ok(new { status = "success" }));
app.MapGet("/sharedutilities/auditorsearchcontroller", () => Results.Ok(new { status = "success" }));
app.MapGet("/sharedutilities/backuphrasearchcontroller", () => Results.Ok(new { status = "success" }));

app.Run();

public partial class Program { }