using Microsoft.EntityFrameworkCore;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Domain.Ports.Repositories;
using Sora_AppProject.Infrastructure.Context;

namespace Sora_AppProject.Infrastructure.Adapters.Repositories.Implementation;

public sealed class SolicitudTallaRepository : ISolicitudTallaRepository
{
    private readonly ApplicationDbContext _context;

    public SolicitudTallaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(SolicitudTalla solicitudTalla)
    {
        await _context.SolicitudTallas.AddAsync(solicitudTalla);
    }

    public async Task<List<SolicitudTalla>> GetAllAsync()
    {
        return await _context.SolicitudTallas
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<SolicitudTalla?> GetByIdAsync(int id)
    {
        return await _context.SolicitudTallas
            .FirstOrDefaultAsync(solicitudTalla => solicitudTalla.Id == id);
    }

    public async Task<List<SolicitudTalla>> GetBySolicitudIdAsync(int solicitudId)
    {
        return await _context.SolicitudTallas
            .AsNoTracking()
            .Where(solicitudTalla => solicitudTalla.SolicitudId == solicitudId)
            .ToListAsync();
    }

    public Task DeleteAsync(SolicitudTalla solicitudTalla)
    {
        _context.SolicitudTallas.Remove(solicitudTalla);
        return Task.CompletedTask;
    }

    public async Task<int> GetTotalCantidadBySolicitudIdAsync(int solicitudId)
    {
        return await _context.SolicitudTallas
            .Where(solicitudTalla => solicitudTalla.SolicitudId == solicitudId)
            .SumAsync(solicitudTalla => solicitudTalla.Cantidad);
    }
}
