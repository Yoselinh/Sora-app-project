using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Domain.Ports.Repositories;

public interface IUsuarioRepository
{
        Task<Usuario?> GetByEmailAsync(string email);
        
        //metodos para addusuario
        Task<Role?> GetRolByNombreAsync(string nombre);
        Task AddAsync(Usuario usuario);
}
