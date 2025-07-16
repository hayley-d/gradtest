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
app.AddSwaggerDoc(builder);
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.OAuthClientId(builder.Configuration["OIDC:ClientId"]);
        options.OAuthUsePkce(); 
    });
}

app.UseHangfireDashboard();
app.UseHangfireServer();  

app.SetupJobs();

app.MapEndpoints();

app.MapGet("/", () => Results.Redirect("/swagger"));

app.UseCors("Application");

app.Run();
