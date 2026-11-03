using Microsoft.EntityFrameworkCore;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Domain.Repositories;
using Sora_AppProject.Infrastructure.Context;

namespace Sora_AppProject.Infrastructure.Repositories.Implementation;

public sealed class SolicitudTallaRepository : ISolicitudTallaRepository
{
    private readonly ApplicationDbContext _context;

    public SolicitudTallaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        SolicitudTalla solicitudTalla,
        CancellationToken cancellationToken = default)
    {
        await _context.SolicitudTallas.AddAsync(solicitudTalla, cancellationToken);
    }

    public async Task<IReadOnlyList<SolicitudTalla>> GetAllAsync(
        CancellationToken cancellationToken = default) =>
        await _context.SolicitudTallas
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public Task<SolicitudTalla?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default) =>
        _context.SolicitudTallas
            .AsNoTracking()
            .FirstOrDefaultAsync(solicitudTalla => solicitudTalla.Id == id, cancellationToken);

    public async Task<IReadOnlyList<SolicitudTalla>> GetBySolicitudIdAsync(
        int solicitudId,
        CancellationToken cancellationToken = default) =>
        await _context.SolicitudTallas
            .AsNoTracking()
            .Where(solicitudTalla => solicitudTalla.SolicitudId == solicitudId)
            .ToListAsync(cancellationToken);

    public Task UpdateAsync(
        SolicitudTalla solicitudTalla,
        CancellationToken cancellationToken = default)
    {
        _context.SolicitudTallas.Update(solicitudTalla);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(
        SolicitudTalla solicitudTalla,
        CancellationToken cancellationToken = default)
    {
        _context.SolicitudTallas.Remove(solicitudTalla);
        return Task.CompletedTask;
    }

    public Task<int> GetTotalCantidadBySolicitudIdAsync(
        int solicitudId,
        CancellationToken cancellationToken = default) =>
        _context.SolicitudTallas
            .Where(solicitudTalla => solicitudTalla.SolicitudId == solicitudId)
            .SumAsync(solicitudTalla => solicitudTalla.Cantidad, cancellationToken);
}
