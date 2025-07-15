using GradTest.Configuration;
using GradTest.Models;
using GradTest.Utils;

var builder = WebApplication.CreateBuilder(args);

builder.SetupServices();
builder.SetupEntityFramework();
builder.SetupLogging();
builder.SetupJobs();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Hello World!");

app.Run();
