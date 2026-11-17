using MediatR;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Domain.Ports.Repositories;
using Sora_AppProject.Domain.Ports.Services;

namespace Sora_AppProject.Application.UseCases.Usuarios.Commands;

public class AddUsuarioCommand : IRequest<Unit>
{
    public string Nombre { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Rol { get; set; } = null!;
}

internal sealed class AddUsuarioCommandHandler(IUnitOfWork unitOfWork, IAuthService authService)
    : IRequestHandler<AddUsuarioCommand, Unit>
{
    public async Task<Unit> Handle(AddUsuarioCommand request, CancellationToken cancellationToken)
    {
        if (request.Rol != "cliente" && request.Rol != "taller")
            throw new ArgumentException("El rol debe ser 'cliente' o 'taller'.");

        if (await unitOfWork.UsuarioRepository.GetByEmailAsync(request.Email) is not null)
            throw new InvalidOperationException("Ya existe un usuario con ese email.");

        var rol = await unitOfWork.UsuarioRepository.GetRolByNombreAsync(request.Rol)
                  ?? throw new KeyNotFoundException("El rol no existe.");

        await unitOfWork.UsuarioRepository.AddAsync(new Usuario
        {
            Nombre = request.Nombre,
            Email = request.Email,
            PasswordHash = authService.HashPassword(request.Password),
            RolId = rol.Id
        });

        await unitOfWork.SaveChangesAsync();
        return Unit.Value;
    }
}