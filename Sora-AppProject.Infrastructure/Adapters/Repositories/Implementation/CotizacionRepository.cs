using Microsoft.EntityFrameworkCore;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Domain.Ports.Repositories;
using Sora_AppProject.Infrastructure.Context;

namespace Sora_AppProject.Infrastructure.Adapters.Repositories.Implementation;

public class CotizacionRepository : ICotizacionRepository
{
    private readonly ApplicationDbContext _context;

    public CotizacionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Cotizacione cotizacion)
    {
        await _context.Cotizaciones.AddAsync(cotizacion);
    }

    public async Task<IReadOnlyList<Cotizacione>> GetAllAsync()
    {
        return await _context.Cotizaciones
            .AsNoTracking()
            .OrderByDescending(c => c.Fecha)
            .ToListAsync();
    }

    public async Task<Cotizacione?> GetByIdAsync(int id)
    {
        return await _context.Cotizaciones
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Cotizacione>> GetBySolicitudIdAsync(int solicitudId)
    {
        return await _context.Cotizaciones
            .Where(c => c.SolicitudId == solicitudId)
            .OrderBy(c => c.Total)
            .ToListAsync();
    }

    public async Task<List<Cotizacione>> GetByTallerIdAsync(int tallerId)
    {
        return await _context.Cotizaciones
            .AsNoTracking()
            .Where(c => c.TallerId == tallerId)
            .OrderByDescending(c => c.Fecha)
            .ToListAsync();
    }

    public async Task<bool> ExistsAsync(int solicitudId, int tallerId)
    {
        return await _context.Cotizaciones
            .AnyAsync(c => c.SolicitudId == solicitudId && c.TallerId == tallerId);
    }
    public Task UpdateAsync(Cotizacione cotizacion)
    {
        _context.Cotizaciones.Update(cotizacion);
        return Task.CompletedTask;
    }


    public Task DeleteAsync(Cotizacione cotizacion)
    {
        _context.Cotizaciones.Remove(cotizacion);
        return Task.CompletedTask;
    }
}