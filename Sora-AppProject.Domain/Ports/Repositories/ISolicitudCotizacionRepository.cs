using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Domain.Ports.Repositories;

public interface ISolicitudCotizacionRepository
{
    Task AddAsync(SolicitudesCotizacion solicitud);

    Task<List<SolicitudesCotizacion>> GetAllAsync();

    Task<SolicitudesCotizacion?> GetByIdAsync(int id);

    Task<List<SolicitudesCotizacion>> GetByClienteIdAsync(int clienteId);

    Task<List<SolicitudesCotizacion>> GetByEstadoAsync(string estado);

    Task<bool> HasCotizacionesAsync(int solicitudId);
    
    Task UpdateAsync(SolicitudesCotizacion solicitud);

    Task DeleteAsync(SolicitudesCotizacion solicitud);
}
