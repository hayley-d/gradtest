using FluentValidation;
using GradTest.Application.Common.Behaviors;
using GradTest.Application.Orders.Commands.CreateOrder;
using GradTest.Models;
using GradTest.Services;
using GradTest.Utils;
using Hangfire;
using Hangfire.PostgreSql;
using MediatR;
using Microsoft.OpenApi.Models;

namespace GradTest.API.Configuration;

public static class ServiceConfiguration
{
    public static void SetupServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        
        builder
            .SetupCors()
            .SetupRucurringJobs()
            .SetupSwaggerDoc()
            .SetupHangfireServices()
            .SetupMediatR();
    }

    private static WebApplicationBuilder SetupCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("Application",
                corsPolicyBuilder => corsPolicyBuilder.WithOrigins("http://localhost:5073/")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
        });
        
        return builder;
    }

    private static void SetupMediatR(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(CreateOrderCommand).Assembly));

        builder.Services.AddValidatorsFromAssemblyContaining<CreateOrderValidator>();

        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
 
    }
    
    private static WebApplicationBuilder SetupSwaggerDoc(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        
        const string displayName = "Grad Test API";
        
        var authorizationUrl = builder.Configuration["OIDC:AuthorizeUrl"]!;
        
        var tokenUrl = builder.Configuration["OIDC:TokenUrl"]!;
        
        builder.Services.AddSwaggerGen(options =>
        {
            options.EnableAnnotations();
            options.SchemaFilter<SmartEnumSchemaFilter<Category>>(); 
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
        
        return builder;
    }
    
    private static WebApplicationBuilder SetupHangfireServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddHangfire(config => config.UsePostgreSqlStorage(ConnectionStrings.GetPostgresConnectionString()));
        builder.Services.AddHangfireServer();
        return builder;
    }

    private static WebApplicationBuilder SetupRucurringJobs(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpClient<IExchangeRateService, ExchangeRateService>();
        builder.Services.AddScoped<IExchangeRateSyncJob, ExchangeRateSyncJob>();
        
        return builder;
    }
    
    public static void ConfigureSwaggerUi(this WebApplication app, IHostApplicationBuilder builder)
    {
        if (!app.Environment.IsDevelopment()) return;
        
        app.UseSwagger();
        
        app.UseSwaggerUI(options =>
        {
            options.OAuthClientId(builder.Configuration["OIDC:ClientId"]);
            options.OAuthUsePkce(); 
            options.OAuthAppName("Grad Test API");
        });
    }
}