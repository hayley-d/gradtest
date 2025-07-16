using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace GradTest.Configuration;

public static class AuthenticationConfiguration
{
    public static void SetupAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
                {
                    options.Authority = builder.Configuration["OIDC:Authority"];
                    options.MetadataAddress = builder.Configuration["OIDC:MetadataAddress"];
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

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy =>
                policy.RequireRole("Admin"));
        });
    }
}