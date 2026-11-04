using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Domain.Ports.Repositories;

public interface IDisenoRepository
{
    Task AddAsync(
        Diseno diseno,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Diseno>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<Diseno?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Diseno>> GetByClienteIdAsync(
        int clienteId,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        Diseno diseno,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        Diseno diseno,
        CancellationToken cancellationToken = default);
}