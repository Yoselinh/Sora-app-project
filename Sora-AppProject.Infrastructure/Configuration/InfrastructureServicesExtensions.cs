using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sora_AppProject.Domain.Repositories;
using Sora_AppProject.Infrastructure.Context;
using Sora_AppProject.Infrastructure.Repositories.Implementation;

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
        services.AddScoped<ITallerRepository, TallerRepository>();
        services.AddScoped<ITallerTelaRepository, TallerTelaRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
