using GradTest.Infrastructure.Persistence;
using GradTest.Utils;
using Microsoft.EntityFrameworkCore;

namespace GradTest.API.Configuration.Builder;

public static class EntityFrameworkConfiguration
{
    public static void SetupEntityFramework(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));
    }

}