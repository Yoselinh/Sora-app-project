using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Domain.Ports.Repositories;

public interface ITallerRepository
{
    Task AddAsync(Tallere taller);

    Task<List<Tallere>> GetAllAsync();

    Task<Tallere?> GetByIdAsync(int id);

    Task<bool> ExistsByUsuarioIdAsync(int usuarioId);

    Task<bool> HasCotizacionesAsync(int tallerId);

    Task DeleteAsync(Tallere taller);
}