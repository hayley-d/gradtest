namespace GradTest.Configuration;
using Microsoft.EntityFrameworkCore;
using GradTest.Persistence;
using GradTest.Models;
using Npgsql.EntityFrameworkCore.PostgreSQL;

public static class EntityFrameworkConfiguration
{
    public static void SetupEntityFramework(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(builder.Configuration["ConnectionString"]));
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(builder.Configuration["ConnectionString"]));
        builder.Services.AddScoped<ApplicationDbContext>();
    
    }

}