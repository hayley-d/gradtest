using Microsoft.AspNetCore.Mvc;

namespace GradTest.API.Middleware;

public class ValidationExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ValidationExceptionMiddleware> _logger;
    
    public ValidationExceptionMiddleware(RequestDelegate next, ILogger<ValidationExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (FluentValidation.ValidationException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            var problemDetails = new ValidationProblemDetails(
                ex.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        group => group.Key,
                        group => group
                            .Select(e => e.ErrorMessage)
                            .ToArray())
            )
            {
                Title = "One or more validation errors occurred.",
                Status = StatusCodes.Status400BadRequest,
            };
            
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}