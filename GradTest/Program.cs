using GradTest.Configuration;
using GradTest.Endpoints;
using GradTest.Models;
using GradTest.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.SetupServices();
builder.SetupEntityFramework();
builder.SetupLogging();


var app = builder.Build();
app.SetupJobs();
app.MapEndpoints();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Hello World!");

app.Run();
