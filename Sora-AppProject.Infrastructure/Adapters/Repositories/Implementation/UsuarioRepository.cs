using Microsoft.EntityFrameworkCore;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Domain.Ports.Repositories;
using Sora_AppProject.Infrastructure.Context;

namespace Sora_AppProject.Infrastructure.Adapters.Repositories.Implementation;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly ApplicationDbContext _context;

    public UsuarioRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> GetByEmailAsync(string email) =>
        await _context.Usuarios
            .Include(u => u.Rol)
            .FirstOrDefaultAsync(u => u.Email == email);
    //adusuario
    public async Task<Role?> GetRolByNombreAsync(string nombre) =>
        await _context.Roles.FirstOrDefaultAsync(r => r.Nombre == nombre);

    public async Task AddAsync(Usuario usuario) =>
        await _context.Usuarios.AddAsync(usuario);
}