using Microsoft.EntityFrameworkCore;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Domain.Repositories;
using Sora_AppProject.Infrastructure.Context;

namespace Sora_AppProject.Infrastructure.Repositories.Implementation;

public sealed class SolicitudCotizacionRepository : ISolicitudCotizacionRepository
{
    private readonly ApplicationDbContext _context;

    public SolicitudCotizacionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        SolicitudesCotizacion solicitud,
        CancellationToken cancellationToken = default)
    {
        await _context.SolicitudesCotizacions.AddAsync(solicitud, cancellationToken);
    }

    public async Task<IReadOnlyList<SolicitudesCotizacion>> GetAllAsync(
        CancellationToken cancellationToken = default) =>
        await _context.SolicitudesCotizacions
            .AsNoTracking()
            .ToListAsync(cancellationToken);

    public Task<SolicitudesCotizacion?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default) =>
        _context.SolicitudesCotizacions
            .AsNoTracking()
            .FirstOrDefaultAsync(solicitud => solicitud.Id == id, cancellationToken);

    public async Task<IReadOnlyList<SolicitudesCotizacion>> GetByClienteIdAsync(
        int clienteId,
        CancellationToken cancellationToken = default) =>
        await _context.SolicitudesCotizacions
            .AsNoTracking()
            .Where(solicitud => solicitud.ClienteId == clienteId)
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<SolicitudesCotizacion>> GetByEstadoAsync(
        string estado,
        CancellationToken cancellationToken = default) =>
        await _context.SolicitudesCotizacions
            .AsNoTracking()
            .Where(solicitud => solicitud.Estado == estado)
            .ToListAsync(cancellationToken);

    public Task UpdateAsync(
        SolicitudesCotizacion solicitud,
        CancellationToken cancellationToken = default)
    {
        _context.SolicitudesCotizacions.Update(solicitud);
        return Task.CompletedTask;
    }
}
