using GradTest.Persistence;
using GradTest.Utils;
using Microsoft.EntityFrameworkCore;

namespace GradTest.API.Configuration.Builder;

public static class EntityFrameworkConfiguration
{
    public static void SetupEntityFramework(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(ConnectionStrings.GetPostgresConnectionString()));
    }

}