using Microsoft.EntityFrameworkCore;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Domain.Repositories;
using Sora_AppProject.Infrastructure.Context;

namespace Sora_AppProject.Infrastructure.Repositories.Implementation;

public sealed class TallerTelaRepository : ITallerTelaRepository
{
    private readonly ApplicationDbContext _context;

    public TallerTelaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        TallerTela tallerTela,
        CancellationToken cancellationToken = default)
    {
        await _context.TallerTelas.AddAsync(tallerTela, cancellationToken);
    }

    // Sin Include para evitar ciclos en el JSON (Tela -> TallerTelas -> Tela).
    public async Task<IReadOnlyList<TallerTela>> GetByTallerIdAsync(
        int tallerId,
        CancellationToken cancellationToken = default) =>
        await _context.TallerTelas
            .AsNoTracking()
            .Where(tt => tt.TallerId == tallerId)
            .ToListAsync(cancellationToken);

    public async Task<TallerTela?> GetByIdAsync(
        int tallerId,
        int telaId,
        CancellationToken cancellationToken = default) =>
        await _context.TallerTelas
            .FirstOrDefaultAsync(
                tt => tt.TallerId == tallerId && tt.TelaId == telaId,
                cancellationToken);

    public async Task<bool> ExistsAsync(
        int tallerId,
        int telaId,
        CancellationToken cancellationToken = default) =>
        await _context.TallerTelas
            .AnyAsync(
                tt => tt.TallerId == tallerId && tt.TelaId == telaId,
                cancellationToken);

    public async Task<bool> TallerExistsAsync(
        int tallerId,
        CancellationToken cancellationToken = default) =>
        await _context.Talleres
            .AnyAsync(t => t.Id == tallerId, cancellationToken);

    public async Task<bool> TelaExistsAsync(
        int telaId,
        CancellationToken cancellationToken = default) =>
        await _context.Telas
            .AnyAsync(t => t.Id == telaId, cancellationToken);

    public Task UpdateAsync(
        TallerTela tallerTela,
        CancellationToken cancellationToken = default)
    {
        _context.TallerTelas.Update(tallerTela);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(
        TallerTela tallerTela,
        CancellationToken cancellationToken = default)
    {
        _context.TallerTelas.Remove(tallerTela);
        return Task.CompletedTask;
    }
}
