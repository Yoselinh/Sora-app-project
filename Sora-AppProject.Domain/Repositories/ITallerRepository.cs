using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Domain.Repositories;

public interface ITallerRepository
{
    Task AddAsync(Tallere taller, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Tallere>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Tallere?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<bool> UsuarioExistsAsync(int usuarioId, CancellationToken cancellationToken = default);

    Task<bool> ExistsByUsuarioIdAsync(int usuarioId, CancellationToken cancellationToken = default);

    Task<bool> HasCotizacionesAsync(int tallerId, CancellationToken cancellationToken = default);

    Task UpdateAsync(Tallere taller, CancellationToken cancellationToken = default);

    Task DeleteAsync(Tallere taller, CancellationToken cancellationToken = default);
}