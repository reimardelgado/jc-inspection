using System.Configuration;
using Backend.Application.Configurations;

namespace Backend.API.Extensions;

public static class IdentityServiceExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
    {
        //JWT config
        services.Configure<JwtAuthenticationSettings>(config.GetSection("JWT"));

        services
            .AddAuthentication("JWT")
            .AddScheme<JwtAuthenticationSchemaOptions, JwtAuthenticationHandler>("JWT", null)
            .AddJwtBearer(options =>
            {
                var jwtKey = Encoding.ASCII.GetBytes(config["Jwt:Secret"]);
                var securityKey = new SymmetricSecurityKey(jwtKey);

                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    TokenDecryptionKey = securityKey,
                    IssuerSigningKey = new SymmetricSecurityKey(jwtKey)
                };
            });
        
        

        return services;
    }
}