using Microsoft.EntityFrameworkCore;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Domain.Ports.Repositories;
using Sora_AppProject.Infrastructure.Context;

namespace Sora_AppProject.Infrastructure.Adapters.Repositories.Implementation;

public class DisenoTelaRepository : IDisenoTelaRepository
{
    private readonly ApplicationDbContext _context;

    public DisenoTelaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(DisenoTela disenoTela)
    {
        await _context.DisenoTelas.AddAsync(disenoTela);
    }

    public async Task<List<DisenoTela>> GetAllAsync()
    {
        return await _context.DisenoTelas
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<DisenoTela?> GetByIdAsync(int id)
    {
        return await _context.DisenoTelas
            .FirstOrDefaultAsync(dt => dt.Id == id);
    }

    public async Task<List<DisenoTela>> GetByDisenoIdAsync(int disenoId)
    {
        return await _context.DisenoTelas
            .AsNoTracking()
            .Where(dt => dt.DisenoId == disenoId)
            .ToListAsync();
    }

    public Task DeleteAsync(DisenoTela disenoTela)
    {
        _context.DisenoTelas.Remove(disenoTela);
        return Task.CompletedTask;
    }
}