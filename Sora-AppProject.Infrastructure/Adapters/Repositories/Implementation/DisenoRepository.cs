using Microsoft.EntityFrameworkCore;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Domain.Repositories;
using Sora_AppProject.Infrastructure.Context;

namespace Sora_AppProject.Infrastructure.Repositories.Implementation;

public sealed class DisenoRepository : IDisenoRepository
{
    private readonly ApplicationDbContext _context;

    public DisenoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Diseno diseno,
        CancellationToken cancellationToken = default)
    {
        await _context.Disenos.AddAsync(
            diseno,
            cancellationToken
        );
    }

    public async Task<IReadOnlyList<Diseno>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Disenos
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Diseno?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Disenos
            .FirstOrDefaultAsync(
                d => d.Id == id,
                cancellationToken
            );
    }

    public async Task<IReadOnlyList<Diseno>> GetByClienteIdAsync(
        int clienteId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Disenos
            .AsNoTracking()
            .Where(d => d.ClienteId == clienteId)
            .ToListAsync(cancellationToken);
    }

    public Task UpdateAsync(
        Diseno diseno,
        CancellationToken cancellationToken = default)
    {
        _context.Disenos.Update(diseno);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(
        Diseno diseno,
        CancellationToken cancellationToken = default)
    {
        _context.Disenos.Remove(diseno);
        return Task.CompletedTask;
    }
}