using MediatR;
using Sora_AppProject.Application.Dtos;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.SolicitudTallas.Queries;

public class GetAllSolicitudTallasQuery : IRequest<List<SolicitudTallaDto>>
{
}

internal sealed class GetAllSolicitudTallasQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllSolicitudTallasQuery, List<SolicitudTallaDto>>
{
    public async Task<List<SolicitudTallaDto>> Handle(
        GetAllSolicitudTallasQuery request,
        CancellationToken cancellationToken)
    {
        var tallas = await unitOfWork.SolicitudTallaRepository.GetAllAsync();

        return tallas.Select(talla => new SolicitudTallaDto
        {
            Id = talla.Id,
            SolicitudId = talla.SolicitudId,
            Talla = talla.Talla,
            Cantidad = talla.Cantidad
        }).ToList();
    }
}
