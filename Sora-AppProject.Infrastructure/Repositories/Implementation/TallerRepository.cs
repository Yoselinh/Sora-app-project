using Microsoft.EntityFrameworkCore;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Domain.Repositories;
using Sora_AppProject.Infrastructure.Context;

namespace Sora_AppProject.Infrastructure.Repositories.Implementation;

public sealed class TallerRepository : ITallerRepository
{
    private readonly ApplicationDbContext _context;

    public TallerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Tallere taller,
        CancellationToken cancellationToken = default)
    {
        await _context.Talleres.AddAsync(taller, cancellationToken);
    }

    public async Task<IReadOnlyList<Tallere>> GetAllAsync(
        CancellationToken cancellationToken = default) =>
        await _context.Talleres
            .AsNoTracking()
            .OrderBy(t => t.Nombre)
            .ToListAsync(cancellationToken);

    public async Task<Tallere?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default) =>
        await _context.Talleres
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

    public async Task<bool> UsuarioExistsAsync(
        int usuarioId,
        CancellationToken cancellationToken = default) =>
        await _context.Usuarios
            .AnyAsync(u => u.Id == usuarioId, cancellationToken);

    public async Task<bool> ExistsByUsuarioIdAsync(
        int usuarioId,
        CancellationToken cancellationToken = default) =>
        await _context.Talleres
            .AnyAsync(t => t.UsuarioId == usuarioId, cancellationToken);

    public async Task<bool> HasCotizacionesAsync(
        int tallerId,
        CancellationToken cancellationToken = default) =>
        await _context.Cotizaciones
            .AnyAsync(c => c.TallerId == tallerId, cancellationToken);

    public Task UpdateAsync(
        Tallere taller,
        CancellationToken cancellationToken = default)
    {
        _context.Talleres.Update(taller);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(
        Tallere taller,
        CancellationToken cancellationToken = default)
    {
        // Primero se quitan las telas que ofrece el taller.
        var telasDelTaller = await _context.TallerTelas
            .Where(tt => tt.TallerId == taller.Id)
            .ToListAsync(cancellationToken);

        _context.TallerTelas.RemoveRange(telasDelTaller);
        _context.Talleres.Remove(taller);
    }
}
