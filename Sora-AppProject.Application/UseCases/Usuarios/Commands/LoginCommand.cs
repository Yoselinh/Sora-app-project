using MediatR;
using Sora_AppProject.Application.Dtos;
using Sora_AppProject.Domain.Ports.Repositories;
using Sora_AppProject.Domain.Ports.Services;

namespace Sora_AppProject.Application.UseCases.Usuarios.Commands;

public class LoginCommand : IRequest<LoginDto>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

internal sealed class LoginCommandHandler(IUnitOfWork unitOfWork, IAuthService authService)
    : IRequestHandler<LoginCommand, LoginDto>
{
    public async Task<LoginDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var usuario = await unitOfWork.UsuarioRepository.GetByEmailAsync(request.Email);

        if (usuario is null || !authService.VerificarPassword(request.Password, usuario.PasswordHash))
            throw new UnauthorizedAccessException("Credenciales inválidas.");

        return new LoginDto
        {
            Token = authService.GenerarToken(usuario, usuario.Rol.Nombre),
            Email = usuario.Email,
            Rol = usuario.Rol.Nombre
        };
    }
}