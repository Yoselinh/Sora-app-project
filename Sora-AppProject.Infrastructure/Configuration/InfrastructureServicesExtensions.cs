using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sora_AppProject.Domain.Ports.Repositories;
using Sora_AppProject.Domain.Ports.Services;
using Sora_AppProject.Infrastructure.Adapters.Repositories.Implementation;
using Sora_AppProject.Infrastructure.Adapters.Services.Implementation;
using Sora_AppProject.Infrastructure.Context;

namespace Sora_AppProject.Infrastructure.Configuration;

public static class InfrastructureServicesExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "La cadena de conexión 'DefaultConnection' no está configurada.");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<ISolicitudCotizacionRepository, SolicitudCotizacionRepository>();
        services.AddScoped<ISolicitudTallaRepository, SolicitudTallaRepository>();

        services.AddScoped<IDisenoRepository, DisenoRepository>();
        services.AddScoped<IDisenoTelaRepository, DisenoTelaRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
