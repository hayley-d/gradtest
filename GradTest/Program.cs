using GradTest.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
Category cat = Category.FOOD;

app.MapGet("/", () => "Hello World!");

app.Run();
