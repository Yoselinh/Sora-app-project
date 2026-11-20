using Microsoft.EntityFrameworkCore;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Domain.Ports.Repositories;
using Sora_AppProject.Infrastructure.Context;

namespace Sora_AppProject.Infrastructure.Adapters.Repositories.Implementation;

public sealed class CotizacionRepository : ICotizacionRepository
{
    private readonly ApplicationDbContext _context;

    public CotizacionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Cotizacione cotizacion, CancellationToken cancellationToken = default)
    {
        await _context.Cotizaciones.AddAsync(cotizacion, cancellationToken);
    }

    public async Task<IReadOnlyList<Cotizacione>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Cotizaciones
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Cotizacione?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Cotizaciones
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public Task UpdateAsync(Cotizacione cotizacion, CancellationToken cancellationToken = default)
    {
        _context.Cotizaciones.Update(cotizacion);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Cotizacione cotizacion, CancellationToken cancellationToken = default)
    {
        _context.Cotizaciones.Remove(cotizacion);
        return Task.CompletedTask;
    }
}
