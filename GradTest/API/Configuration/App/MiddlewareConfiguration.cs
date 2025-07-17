using GradTest.API.Middleware;

namespace GradTest.API.Configuration.App;

public static class MiddlewareConfiguration
{
    public static void ConfigureMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ValidationExceptionMiddleware>();
    } 
}