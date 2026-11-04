using Microsoft.EntityFrameworkCore;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Domain.Repositories;
using Sora_AppProject.Infrastructure.Context;

namespace Sora_AppProject.Infrastructure.Repositories.Implementation;

public sealed class DisenoTelaRepository : IDisenoTelaRepository
{
    private readonly ApplicationDbContext _context;

    public DisenoTelaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        DisenoTela disenoTela,
        CancellationToken cancellationToken = default)
    {
        await _context.DisenoTelas.AddAsync(
            disenoTela,
            cancellationToken
        );
    }

    public async Task<IReadOnlyList<DisenoTela>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.DisenoTelas
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<DisenoTela?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await _context.DisenoTelas
            .FirstOrDefaultAsync(
                dt => dt.Id == id,
                cancellationToken
            );
    }

    public async Task<IReadOnlyList<DisenoTela>> GetByDisenoIdAsync(
        int disenoId,
        CancellationToken cancellationToken = default)
    {
        return await _context.DisenoTelas
            .AsNoTracking()
            .Where(dt => dt.DisenoId == disenoId)
            .ToListAsync(cancellationToken);
    }

    public Task UpdateAsync(
        DisenoTela disenoTela,
        CancellationToken cancellationToken = default)
    {
        _context.DisenoTelas.Update(disenoTela);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(
        DisenoTela disenoTela,
        CancellationToken cancellationToken = default)
    {
        _context.DisenoTelas.Remove(disenoTela);
        return Task.CompletedTask;
    }
}