using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Domain.Ports.Repositories;

public interface ISolicitudTallaRepository
{
    Task AddAsync(SolicitudTalla solicitudTalla);

    Task<List<SolicitudTalla>> GetAllAsync();

    Task<SolicitudTalla?> GetByIdAsync(int id);

    Task<List<SolicitudTalla>> GetBySolicitudIdAsync(int solicitudId);

    Task DeleteAsync(SolicitudTalla solicitudTalla);

    Task<int> GetTotalCantidadBySolicitudIdAsync(int solicitudId);
}
