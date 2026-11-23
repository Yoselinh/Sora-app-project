using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Domain.Ports.Repositories;

public interface IDisenoRepository
{
    Task AddAsync(Diseno diseno);

    Task<List<Diseno>> GetAllAsync();

    Task<Diseno?> GetByIdAsync(int id);

    Task<List<Diseno>> GetByClienteIdAsync(int clienteId);

    Task DeleteAsync(Diseno diseno);
}