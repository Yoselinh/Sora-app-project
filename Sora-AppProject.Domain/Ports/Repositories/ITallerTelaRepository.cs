using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Domain.Ports.Repositories;

public interface ITallerTelaRepository
{
    Task AddAsync(TallerTela tallerTela);

    Task<List<TallerTela>> GetByTallerIdAsync(int tallerId);

    Task<TallerTela?> GetByIdAsync(int tallerId, int telaId);

    Task<bool> ExistsAsync(int tallerId, int telaId);

    Task<bool> TelaExistsAsync(int telaId);

    Task DeleteAsync(TallerTela tallerTela);
}