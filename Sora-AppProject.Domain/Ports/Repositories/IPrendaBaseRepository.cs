using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Domain.Ports.Repositories;

public interface IPrendaBaseRepository
{
    Task AddAsync(PrendasBase prendaBase);
    Task<IReadOnlyList<PrendasBase>> GetAllAsync();
    Task<PrendasBase?> GetByIdAsync(int id);
    Task UpdateAsync(PrendasBase prendaBase);
    Task DeleteAsync(PrendasBase prendaBase);
}
