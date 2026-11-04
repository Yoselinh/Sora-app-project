using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Domain.Ports.Services;

public interface IAuthService
{
    string GenerarToken(Usuario usuario, string rol);
    string HashPassword(string password);
    bool VerificarPassword(string password, string hash);
}