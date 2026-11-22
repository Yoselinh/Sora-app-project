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

    public async Task AddAsync(Tela tela, CancellationToken cancellationToken = default)
    {
        await _context.Telas.AddAsync(tela, cancellationToken);
    }

    public async Task<IReadOnlyList<Tela>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Telas
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Tela?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Telas
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public Task UpdateAsync(Tela tela, CancellationToken cancellationToken = default)
    {
        _context.Telas.Update(tela);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Tela tela, CancellationToken cancellationToken = default)
    {
        _context.Telas.Remove(tela);
        return Task.CompletedTask;
    }
}
