using Microsoft.EntityFrameworkCore;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Domain.Ports.Repositories;
using Sora_AppProject.Infrastructure.Context;

namespace Sora_AppProject.Infrastructure.Adapters.Repositories.Implementation;

public class DisenoRepository : IDisenoRepository
{
    private readonly ApplicationDbContext _context;

    public DisenoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Diseno diseno)
    {
        await _context.Disenos.AddAsync(diseno);
    }

    public async Task<List<Diseno>> GetAllAsync()
    {
        return await _context.Disenos
            .AsNoTracking()
            .OrderBy(d => d.Nombre)
            .ToListAsync();
    }

    public async Task<Diseno?> GetByIdAsync(int id)
    {
        return await _context.Disenos
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<List<Diseno>> GetByClienteIdAsync(int clienteId)
    {
        return await _context.Disenos
            .AsNoTracking()
            .Where(d => d.ClienteId == clienteId)
            .ToListAsync();
    }

    public Task DeleteAsync(Diseno diseno)
    {
        _context.Disenos.Remove(diseno);
        return Task.CompletedTask;
    }
}