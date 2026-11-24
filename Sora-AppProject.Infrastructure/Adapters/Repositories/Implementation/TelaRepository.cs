using Microsoft.EntityFrameworkCore;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Domain.Ports.Repositories;
using Sora_AppProject.Infrastructure.Context;

namespace Sora_AppProject.Infrastructure.Adapters.Repositories.Implementation;

public sealed class TelaRepository : ITelaRepository
{
    private readonly ApplicationDbContext _context;

    public TelaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Tela tela)
    {
        await _context.Telas.AddAsync(tela);
    }

    public async Task<IReadOnlyList<Tela>> GetAllAsync()
    {
        return await _context.Telas
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Tela?> GetByIdAsync(int id)
    {
        return await _context.Telas
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public Task UpdateAsync(Tela tela)
    {
        _context.Telas.Update(tela);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Tela tela)
    {
        _context.Telas.Remove(tela);
        return Task.CompletedTask;
    }
}
