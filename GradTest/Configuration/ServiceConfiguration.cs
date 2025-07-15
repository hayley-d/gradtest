using GradTest.Models;
using GradTest.Services;
using GradTest.Utils;
using Hangfire;
using Hangfire.PostgreSql;

namespace GradTest.Configuration;

public static class ServiceConfiguration
{
    public static void SetupServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpClient<IExchangeRateService, ExchangeRateService>();
        builder.Services.AddScoped<IExchangeRateSyncJob, ExchangeRateSyncJob>();    
        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(x =>
        {
            x.EnableAnnotations();
            x.SchemaFilter<SmartEnumSchemaFilter<Category>>();
        });
        builder.Services.AddHangfire(config => config.UsePostgreSqlStorage(ConnectionStrings.GetPostgresConnectionString()));
        builder.Services.AddHangfireServer();
    }  
}