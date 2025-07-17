using GradTest.API.Configuration;
using GradTest.API.Endpoints;
using GradTest.Configuration;
using GradTest.Endpoints;
using GradTest.Utils;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

builder.SetupAuthentication();
builder.SetupEntityFramework();
builder.SetupLogging();
builder.SetupServices();

var app = builder.Build();

//app.UseMiddleware<AuthenticationMiddleware>();
app.ConfigureMiddleware();
app.ConfigureSwaggerUi(builder);
app.UseHttpsRedirection();

app.UseHangfireDashboard();
app.UseHangfireServer();  

app.SetupJobs();

app.MapEndpoints();

app.UseCors("Application");

app.Run();
