using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Domain.Repositories;

public interface ITallerTelaRepository
{
    Task AddAsync(TallerTela tallerTela, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TallerTela>> GetByTallerIdAsync(
        int tallerId,
        CancellationToken cancellationToken = default);

    Task<TallerTela?> GetByIdAsync(
        int tallerId,
        int telaId,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(
        int tallerId,
        int telaId,
        CancellationToken cancellationToken = default);

    Task<bool> TallerExistsAsync(int tallerId, CancellationToken cancellationToken = default);

    Task<bool> TelaExistsAsync(int telaId, CancellationToken cancellationToken = default);

    Task UpdateAsync(TallerTela tallerTela, CancellationToken cancellationToken = default);

    Task DeleteAsync(TallerTela tallerTela, CancellationToken cancellationToken = default);
}