using MediatR;
using Sora_AppProject.Application.Dtos;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.Talleres.Queries;

public class GetAllTalleresQuery : IRequest<List<TallerDto>>
{
}

internal sealed class GetAllTalleresQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllTalleresQuery, List<TallerDto>>
{
    public async Task<List<TallerDto>> Handle(GetAllTalleresQuery request, CancellationToken cancellationToken)
    {
        var talleres = await unitOfWork.TallerRepository.GetAllAsync();

        return talleres.Select(t => new TallerDto
        {
            Id = t.Id,
            UsuarioId = t.UsuarioId,
            Nombre = t.Nombre,
            Direccion = t.Direccion,
            Telefono = t.Telefono
        }).ToList();
    }
}