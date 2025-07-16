using System.Text.Json;

namespace GradTest.Utils;

public class AuthenticationMiddleware : IHttpContextHandler
{
    private readonly RequestDelegate _next;
    public bool IsReusable { get; } = true;

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        var authorized = httpContext.Request.Path.StartsWithSegments("/admin")
            ? await AdminAuthorize(httpContext)
            : await UserAuthorize(httpContext);
        if (!authorized)
        {
            httpContext.Response.StatusCode = 401;
            await httpContext.Response.WriteAsync($"Unauthorized: This endpoint is for admin users only.");
        } 
        // Passes to next middleware
        await _next(httpContext);
    }

    public static async Task<String?> GetUserID(HttpContext httpContext)
    {
        var value = httpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;

        if (value is null)
        {
            httpContext.Response.StatusCode = 401;
            await httpContext.Response.WriteAsync($"Unauthorized: This endpoint is for authorized users only.");
        }

        return value;
    }

    public static async Task<bool> AdminAuthorize(HttpContext context)
    {
            bool authorized = false;
            var roleClaimString = context.User.FindFirst("realm_access")?.Value;
            if (roleClaimString is not null)
            {
                var jsonRoleClaim = JsonSerializer.Deserialize<JsonElement>(roleClaimString);
            
                if (jsonRoleClaim.TryGetProperty("roles", out JsonElement roleElement))
                {
                        
                    foreach (var role in roleElement.EnumerateArray())
                    {
                        var roleName = role.GetString();
                        if (roleName is not null && roleName.Equals("grad-admin"))
                        {
                            authorized = true;
                        }
                    }
                }
            }

            if (!authorized)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync($"Unauthorized: This endpoint is for admin users only.");
            }
            
            return authorized;
    }

    public static async Task<bool> UserAuthorize(HttpContext httpContext)
    {
            bool authorized = false;
                    
            var roleClaimString = httpContext.User.FindFirst("realm_access")?.Value;
                    
            if (roleClaimString is not null)
            {
                var jsonRoleClaim = JsonSerializer.Deserialize<JsonElement>(roleClaimString);
            
                if (jsonRoleClaim.TryGetProperty("roles", out JsonElement roleElement))
                {
                        
                    foreach (var role in roleElement.EnumerateArray())
                    {
                        var roleName = role.GetString();
                        if (roleName is not null && roleName.Equals("grad-admin"))
                        {
                            authorized = true;
                        }
                    }
                }
            }

            if (!authorized)
            {
                httpContext.Response.StatusCode = 401;
                await httpContext.Response.WriteAsync($"Unauthorized: This endpoint is for authorized users only.");
            }

            return authorized;
    }
    
    public void ProcessRequest(HttpContext context)
    {
        // TODO: Auth logic 
        
    }
}