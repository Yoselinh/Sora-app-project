using MediatR;
using Sora_AppProject.Application.Dtos;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.Usuarios.Queries;

public class GetAllUsuariosQuery : IRequest<List<UsuarioDto>>
{
}

internal sealed class GetAllUsuariosQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllUsuariosQuery, List<UsuarioDto>>
{
    public async Task<List<UsuarioDto>> Handle(GetAllUsuariosQuery request, CancellationToken cancellationToken)
    {
        var usuarios = await unitOfWork.UsuarioRepository.GetAllAsync();

        return usuarios.Select(u => new UsuarioDto
        {
            Id = u.Id,
            Nombre = u.Nombre,
            Email = u.Email,
            Rol = u.Rol.Nombre
        }).ToList();
    }

}
