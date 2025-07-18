using GradTest.API.Configuration.Authentication;
using GradTest.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace GradTest.API.Configuration.Builder;

public static class AuthenticationConfiguration
{
    public static void SetupAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        RoleClaimType = ClaimsConstants.Role 
                    };
                    
                    options.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = KeycloakRolesProcessor.Process
                    };
                    
                    options.Authority = builder.Configuration["OIDC:Authority"];
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["OIDC:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["OIDC:Audience"],
                    };
                }
            );

        builder.Services
            .AddAuthorizationBuilder()
            .AddPolicy("Admin", policy =>
            {
                policy.RequireRole("admin");
                policy.RequireAuthenticatedUser();
            });
    }
}