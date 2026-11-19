using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Domain.Ports.Repositories;

public interface ISolicitudTallaRepository
{
    Task AddAsync(SolicitudTalla solicitudTalla, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<SolicitudTalla>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<SolicitudTalla?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<SolicitudTalla>> GetBySolicitudIdAsync(
        int solicitudId,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(SolicitudTalla solicitudTalla, CancellationToken cancellationToken = default);

    Task DeleteAsync(SolicitudTalla solicitudTalla, CancellationToken cancellationToken = default);

    Task<int> GetTotalCantidadBySolicitudIdAsync(
        int solicitudId,
        CancellationToken cancellationToken = default);
}
