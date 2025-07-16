using GradTest.Configuration;
using GradTest.Endpoints;
using GradTest.Models;
using GradTest.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.SetupAuthentication();
builder.SetupServices();
builder.SetupEntityFramework();
builder.SetupLogging();

var app = builder.Build();
app.SetupJobs();

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
app.MapEndpoints();

app.MapGet("/", () => Results.Redirect("/swagger"));
app.UseCors("Application");

app.Run();
