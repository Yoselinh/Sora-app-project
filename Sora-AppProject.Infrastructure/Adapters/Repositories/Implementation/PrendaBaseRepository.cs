using Microsoft.EntityFrameworkCore;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Domain.Ports.Repositories;
using Sora_AppProject.Infrastructure.Context;

namespace Sora_AppProject.Infrastructure.Adapters.Repositories.Implementation;

public sealed class PrendaBaseRepository : IPrendaBaseRepository
{
    private readonly ApplicationDbContext _context;

    public PrendaBaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(PrendasBase prendaBase, CancellationToken cancellationToken = default)
    {
        await _context.PrendasBases.AddAsync(prendaBase, cancellationToken);
    }

    public async Task<IReadOnlyList<PrendasBase>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.PrendasBases
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<PrendasBase?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.PrendasBases
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public Task UpdateAsync(PrendasBase prendaBase, CancellationToken cancellationToken = default)
    {
        _context.PrendasBases.Update(prendaBase);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(PrendasBase prendaBase, CancellationToken cancellationToken = default)
    {
        _context.PrendasBases.Remove(prendaBase);
        return Task.CompletedTask;
    }
}
