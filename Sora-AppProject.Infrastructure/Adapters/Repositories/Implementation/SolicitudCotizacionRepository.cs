using Microsoft.EntityFrameworkCore;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Domain.Ports.Repositories;
using Sora_AppProject.Infrastructure.Context;

namespace Sora_AppProject.Infrastructure.Adapters.Repositories.Implementation;

public sealed class SolicitudCotizacionRepository : ISolicitudCotizacionRepository
{
    private readonly ApplicationDbContext _context;

    public SolicitudCotizacionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(SolicitudesCotizacion solicitud)
    {
        await _context.SolicitudesCotizacions.AddAsync(solicitud);
    }

    public async Task<List<SolicitudesCotizacion>> GetAllAsync()
    {
        return await _context.SolicitudesCotizacions
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<SolicitudesCotizacion?> GetByIdAsync(int id)
    {
        return await _context.SolicitudesCotizacions
            .FirstOrDefaultAsync(solicitud => solicitud.Id == id);
    }

    public async Task<List<SolicitudesCotizacion>> GetByClienteIdAsync(int clienteId)
    {
        return await _context.SolicitudesCotizacions
            .AsNoTracking()
            .Where(solicitud => solicitud.ClienteId == clienteId)
            .ToListAsync();
    }

    public async Task<List<SolicitudesCotizacion>> GetByEstadoAsync(string estado)
    {
        return await _context.SolicitudesCotizacions
            .AsNoTracking()
            .Where(solicitud => solicitud.Estado == estado)
            .ToListAsync();
    }

    public async Task<bool> HasCotizacionesAsync(int solicitudId)
    {
        return await _context.Cotizaciones
            .AnyAsync(cotizacion => cotizacion.SolicitudId == solicitudId);
    }

    public Task UpdateAsync(SolicitudesCotizacion solicitud)
    {
        _context.SolicitudesCotizacions.Update(solicitud);
        return Task.CompletedTask;
    }
    
    public Task DeleteAsync(SolicitudesCotizacion solicitud)
    {
        _context.SolicitudesCotizacions.Remove(solicitud);
        return Task.CompletedTask;
    }
}
