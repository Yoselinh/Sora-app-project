using MediatR;
using Sora_AppProject.Application.Dtos;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.Usuarios.Queries;

public class GetUsuarioByIdQuery : IRequest<UsuarioDto>
{
    public int Id { get; set; }
}

internal sealed class GetUsuarioByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetUsuarioByIdQuery, UsuarioDto>
{
    public async Task<UsuarioDto> Handle(GetUsuarioByIdQuery request, CancellationToken cancellationToken)
    {
        var usuario = await unitOfWork.UsuarioRepository.GetByIdAsync(request.Id)
                      ?? throw new KeyNotFoundException("Usuario no encontrado.");

        return new UsuarioDto
        {
            Id = usuario.Id,
            Nombre = usuario.Nombre,
            Email = usuario.Email,
            Rol = usuario.Rol.Nombre
        };
    }
}