using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Domain.Ports.Repositories;

public interface IDisenoTelaRepository
{
    Task AddAsync(DisenoTela disenoTela);

    Task<List<DisenoTela>> GetAllAsync();

    Task<DisenoTela?> GetByIdAsync(int id);

    Task<List<DisenoTela>> GetByDisenoIdAsync(int disenoId);

    Task DeleteAsync(DisenoTela disenoTela);
}