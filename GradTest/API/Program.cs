using GradTest.API.Configuration.App;
using GradTest.API.Configuration.Builder;
using GradTest.API.Endpoints;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
builder.SetupAuthentication();
builder.SetupEntityFramework();
builder.SetupLogging();
builder.SetupServices();
builder.SetupValidatiors();

var app = builder.Build();

app.ConfigureMiddleware();
app.ConfigureSwaggerUi(builder);
app.UseHttpsRedirection();

app.UseHangfireDashboard();
app.UseHangfireServer();  

app.SetupJobs();

app.MapEndpoints();

app.UseCors("Application");

app.Run();
