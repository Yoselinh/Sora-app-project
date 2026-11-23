using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Domain.Ports.Repositories;

public interface IPrendaBaseRepository
{
    Task AddAsync(PrendasBase prendaBase, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PrendasBase>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PrendasBase?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task UpdateAsync(PrendasBase prendaBase, CancellationToken cancellationToken = default);
    Task DeleteAsync(PrendasBase prendaBase, CancellationToken cancellationToken = default);
}
