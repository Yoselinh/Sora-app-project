namespace Sora_AppProject.Application.Dtos;

public class LoginDto
{
    public string Token { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Rol { get; set; } = null!;
}