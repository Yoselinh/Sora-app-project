using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Domain.Ports.Repositories;

public interface ICotizacionRepository
{
    Task AddAsync(Cotizacione cotizacion);
    Task<IReadOnlyList<Cotizacione>> GetAllAsync();
    Task<Cotizacione?> GetByIdAsync(int id);
    Task UpdateAsync(Cotizacione cotizacion);
    Task DeleteAsync(Cotizacione cotizacion);
}
