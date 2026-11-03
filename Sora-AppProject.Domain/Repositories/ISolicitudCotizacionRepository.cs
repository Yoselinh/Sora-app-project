using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Domain.Repositories;

public interface ISolicitudCotizacionRepository
{
    Task AddAsync(SolicitudesCotizacion solicitud, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<SolicitudesCotizacion>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<SolicitudesCotizacion?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<SolicitudesCotizacion>> GetByClienteIdAsync(
        int clienteId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<SolicitudesCotizacion>> GetByEstadoAsync(
        string estado,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(SolicitudesCotizacion solicitud, CancellationToken cancellationToken = default);
}
