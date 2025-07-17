namespace GradTest.Configuration;
using Microsoft.EntityFrameworkCore;
using GradTest.Persistence;
using GradTest.Utils;

public static class EntityFrameworkConfiguration
{
    public static void SetupEntityFramework(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(ConnectionStrings.GetPostgresConnectionString()));
    }

}