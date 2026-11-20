using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;


namespace Sora_AppProject.Configuration;

public static class ServiceRegistrationExtensions
{
    // Configuración de autenticación JWT
        public static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwtKey = configuration["Jwt:Key"]
                ?? throw new InvalidOperationException(
                    "Jwt:Key no está configurado en appsettings.json.");

            var issuer = configuration["Jwt:Issuer"]
                ?? throw new InvalidOperationException(
                    "Jwt:Issuer no está configurado en appsettings.json.");

            var audience = configuration["Jwt:Audience"]
                ?? throw new InvalidOperationException(
                    "Jwt:Audience no está configurado en appsettings.json.");

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme =
                        JwtBearerDefaults.AuthenticationScheme;

                    options.DefaultChallengeScheme =
                        JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,

                            ValidIssuer = issuer,
                            ValidAudience = audience,

                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(jwtKey))
                        };
                });

            services.AddAuthorization();

            return services;
        }

        // Configuración de Swagger con autenticación JWT
        public static IServiceCollection AddSwagger(
            this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Sora API",
                    Version = "v1",
                    Description = "API del proyecto Sora"
                });

                // Definición del esquema de seguridad Bearer
                options.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "Pega solo el token JWT, sin la palabra Bearer."
                    });

                // Requisito global de autenticación
                options.AddSecurityRequirement(document =>
                    new OpenApiSecurityRequirement
                    {
                        [new OpenApiSecuritySchemeReference(
                            "Bearer", document)] = new List<string>()
                    });
            });

            return services;
        }
}
