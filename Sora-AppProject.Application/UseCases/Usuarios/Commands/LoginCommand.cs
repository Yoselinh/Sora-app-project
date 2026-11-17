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

internal sealed class LoginCommandHandler : IRequestHandler<LoginCommand, LoginDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthService _authService;

    public LoginCommandHandler(IUnitOfWork unitOfWork, IAuthService authService)
    {
        _unitOfWork = unitOfWork;
        _authService = authService;
    }

    public async Task<LoginDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _unitOfWork.UsuarioRepository.GetByEmailAsync(request.Email);

        if (usuario is null || !_authService.VerificarPassword(request.Password, usuario.PasswordHash))
            throw new UnauthorizedAccessException("Credenciales inválidas.");

        var token = _authService.GenerarToken(usuario, usuario.Rol.Nombre);

        return new LoginDto()
        {
            Token = token,
            Email = usuario.Email,
            Rol = usuario.Rol.Nombre
        };
    }
}