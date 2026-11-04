using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Domain.Repositories;

public interface IDisenoTelaRepository
{
    Task AddAsync(
        DisenoTela disenoTela,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<DisenoTela>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<DisenoTela?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<DisenoTela>> GetByDisenoIdAsync(
        int disenoId,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        DisenoTela disenoTela,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        DisenoTela disenoTela,
        CancellationToken cancellationToken = default);
}