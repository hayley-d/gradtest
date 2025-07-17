using GradTest.API.Configuration;
using GradTest.API.Configuration.App;
using GradTest.API.Configuration.Builder;
using GradTest.API.Endpoints;
using GradTest.Endpoints;
using GradTest.Utils;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

builder.SetupAuthentication();
builder.SetupEntityFramework();
builder.SetupLogging();
builder.SetupServices();
builder.SetupValidatiors();

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
