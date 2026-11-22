using Microsoft.EntityFrameworkCore;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Domain.Ports.Repositories;
using Sora_AppProject.Infrastructure.Context;

namespace Sora_AppProject.Infrastructure.Adapters.Repositories.Implementation;

public class TallerRepository : ITallerRepository
{
    private readonly ApplicationDbContext _context;

    public TallerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Tallere taller)
    {
        await _context.Talleres.AddAsync(taller);
    }

    public async Task<List<Tallere>> GetAllAsync()
    {
        return await _context.Talleres
            .AsNoTracking()
            .OrderBy(t => t.Nombre)
            .ToListAsync();
    }

    public async Task<Tallere?> GetByIdAsync(int id)
    {
        return await _context.Talleres
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<bool> ExistsByUsuarioIdAsync(int usuarioId)
    {
        return await _context.Talleres
            .AnyAsync(t => t.UsuarioId == usuarioId);
    }

    public async Task<bool> HasCotizacionesAsync(int tallerId)
    {
        return await _context.Cotizaciones
            .AnyAsync(c => c.TallerId == tallerId);
    }

    public async Task DeleteAsync(Tallere taller)
    {
        // Primero se quitan las telas que ofrece el taller.
        var telasDelTaller = await _context.TallerTelas
            .Where(tt => tt.TallerId == taller.Id)
            .ToListAsync();

        _context.TallerTelas.RemoveRange(telasDelTaller);
        _context.Talleres.Remove(taller);
    }
}