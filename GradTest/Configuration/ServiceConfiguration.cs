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
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("Application",
                corsPolicyBuilder => corsPolicyBuilder.WithOrigins("http://localhost:5073/")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
        });
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
        SetupSwaggerDoc(builder);
    }  
    
    public static void SetupSwaggerDoc(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        const string displayName = "Grad Test API";
        
        var authorizationUrl = builder.Configuration["OIDC:AuthorizeUrl"]!;
        var tokenUrl = builder.Configuration["OIDC:TokenUrl"]!;
        
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = displayName,
                Version = "v1"
            });
    
            options.AddSecurityDefinition("OIDC", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    AuthorizationCode = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri(authorizationUrl),
                        TokenUrl = new Uri(tokenUrl),
                        Scopes = new Dictionary<string, string>
                        {
                            { "openid", "OpenID Connect scope" },
                            { "profile", "Profile scope" },
                            { "email", "Email scope" }
                        }
                    }
                }
            });
    
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "OIDC"
                        }
                    },
                    new[] { "openid", "profile", "email" }
                }
            });
        });
    }

    public static void AddSwaggerDoc(this WebApplication app, IHostApplicationBuilder builder)
    {
        if (app.Environment.IsDevelopment())
        {
            var clientId = builder.Configuration["OIDC:ClientId"]!;
        
            ArgumentNullException.ThrowIfNull(clientId);
        
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.OAuthClientId(clientId);
                c.OAuthAppName("Grad Test API");
                c.OAuthUsePkce();
            });
        }
    }
}