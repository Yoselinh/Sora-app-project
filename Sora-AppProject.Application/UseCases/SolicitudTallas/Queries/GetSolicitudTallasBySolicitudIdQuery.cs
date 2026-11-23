using MediatR;
using Sora_AppProject.Application.Dtos;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.SolicitudTallas.Queries;

public class GetSolicitudTallasBySolicitudIdQuery : IRequest<List<SolicitudTallaDto>>
{
    public int SolicitudId { get; set; }
}

internal sealed class GetSolicitudTallasBySolicitudIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetSolicitudTallasBySolicitudIdQuery, List<SolicitudTallaDto>>
{
    public async Task<List<SolicitudTallaDto>> Handle(
        GetSolicitudTallasBySolicitudIdQuery request,
        CancellationToken cancellationToken)
    {
        var tallas = await unitOfWork.SolicitudTallaRepository.GetBySolicitudIdAsync(request.SolicitudId);

        return tallas.Select(talla => new SolicitudTallaDto
        {
            Id = talla.Id,
            SolicitudId = talla.SolicitudId,
            Talla = talla.Talla,
            Cantidad = talla.Cantidad
        }).ToList();
    }
}
