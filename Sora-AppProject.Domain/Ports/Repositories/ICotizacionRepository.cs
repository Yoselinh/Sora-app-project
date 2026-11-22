using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Domain.Ports.Repositories;

public interface ICotizacionRepository
{
    Task AddAsync(Cotizacione cotizacion, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Cotizacione>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Cotizacione?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task UpdateAsync(Cotizacione cotizacion, CancellationToken cancellationToken = default);
    Task DeleteAsync(Cotizacione cotizacion, CancellationToken cancellationToken = default);
}
