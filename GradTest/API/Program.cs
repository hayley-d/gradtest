using GradTest.API.Configuration;
using GradTest.Configuration;
using GradTest.Endpoints;
using GradTest.Models;
using GradTest.Utils;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

builder.SetupAuthentication();
builder.SetupEntityFramework();
builder.SetupLogging();
builder.SetupServices();

var app = builder.Build();

//app.UseMiddleware<AuthenticationMiddleware>();
app.ConfigureSwaggerUi(builder);
app.UseHttpsRedirection();

app.UseHangfireDashboard();
app.UseHangfireServer();  

app.SetupJobs();

app.MapEndpoints();

app.MapGet("/", () => Results.Redirect("/swagger"));

app.UseCors("Application");

app.Run();
