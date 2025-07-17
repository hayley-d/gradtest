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
    }
}