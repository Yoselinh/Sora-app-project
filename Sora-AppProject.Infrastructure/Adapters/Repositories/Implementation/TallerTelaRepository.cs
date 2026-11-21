using Microsoft.EntityFrameworkCore;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Domain.Ports.Repositories;
using Sora_AppProject.Infrastructure.Context;

namespace Sora_AppProject.Infrastructure.Adapters.Repositories.Implementation;

public class TallerTelaRepository : ITallerTelaRepository
{
    private readonly ApplicationDbContext _context;

    public TallerTelaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(TallerTela tallerTela)
    {
        await _context.TallerTelas.AddAsync(tallerTela);
    }

    public async Task<List<TallerTela>> GetByTallerIdAsync(int tallerId)
    {
        return await _context.TallerTelas
            .AsNoTracking()
            .Where(tt => tt.TallerId == tallerId)
            .ToListAsync();
    }

    public async Task<TallerTela?> GetByIdAsync(int tallerId, int telaId)
    {
        return await _context.TallerTelas
            .FirstOrDefaultAsync(tt => tt.TallerId == tallerId && tt.TelaId == telaId);
    }

    public async Task<bool> ExistsAsync(int tallerId, int telaId)
    {
        return await _context.TallerTelas
            .AnyAsync(tt => tt.TallerId == tallerId && tt.TelaId == telaId);
    }

    public async Task<bool> TelaExistsAsync(int telaId)
    {
        return await _context.Telas
            .AnyAsync(t => t.Id == telaId);
    }

    public Task DeleteAsync(TallerTela tallerTela)
    {
        _context.TallerTelas.Remove(tallerTela);
        return Task.CompletedTask;
    }
}