using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Domain.Ports.Repositories;

public interface ITelaRepository
{
    Task AddAsync(Tela tela);
    Task<IReadOnlyList<Tela>> GetAllAsync();
    Task<Tela?> GetByIdAsync(int id);
    Task UpdateAsync(Tela tela);
    Task DeleteAsync(Tela tela);
}
