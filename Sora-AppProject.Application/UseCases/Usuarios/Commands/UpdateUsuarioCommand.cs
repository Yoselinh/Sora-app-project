using MediatR;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.Usuarios.Commands;

public class UpdateUsuarioCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Rol { get; set; } = null!;
}

internal sealed class UpdateUsuarioCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateUsuarioCommand, Unit>
{
    public async Task<Unit> Handle(UpdateUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuario = await unitOfWork.UsuarioRepository.GetByIdAsync(request.Id)
                      ?? throw new KeyNotFoundException("Usuario no encontrado.");

        var otro = await unitOfWork.UsuarioRepository.GetByEmailAsync(request.Email);
        if (otro is not null && otro.Id != usuario.Id)
            throw new InvalidOperationException("Ya existe un usuario con ese email.");

        var rol = await unitOfWork.UsuarioRepository.GetRolByNombreAsync(request.Rol)
                  ?? throw new KeyNotFoundException("El rol no existe.");

        usuario.Nombre = request.Nombre;
        usuario.Email = request.Email;
        usuario.RolId = rol.Id;

        await unitOfWork.SaveChangesAsync();
        return Unit.Value;
    }
}