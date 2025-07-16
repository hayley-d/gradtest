using GradTest.Models;
using GradTest.Services;
using GradTest.Utils;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.OpenApi.Models;

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
        
        builder.Services.AddSwaggerGen(c =>
        {
            c.EnableAnnotations();

            c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri(builder.Configuration["OIDC:AuthorizeUrl"]),
                        TokenUrl = new Uri(builder.Configuration["OIDC:TokenUrl"]),
                        Scopes = new Dictionary<string, string>
                        {
                            { "openid", "OpenID" },
                            { "profile", "User profile" },
                            { "email", "Email address" },
                            { "roles", "User roles" }
                        }
                    }
                }
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "oauth2"
                        }
                    },
                    new[] { "openid", "profile", "email", "roles" }
                }
            });
        });
    }  
}