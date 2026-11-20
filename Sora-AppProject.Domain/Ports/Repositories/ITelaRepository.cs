using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Domain.Ports.Repositories;

public interface ITelaRepository
{
    Task AddAsync(Tela tela, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Tela>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Tela?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task UpdateAsync(Tela tela, CancellationToken cancellationToken = default);
    Task DeleteAsync(Tela tela, CancellationToken cancellationToken = default);
}
