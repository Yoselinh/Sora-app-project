using MediatR;
using Sora_AppProject.Application.Dtos;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.SolicitudesCotizacion.Queries;

public class GetSolicitudesCotizacionByUsuarioQuery : IRequest<List<SolicitudCotizacionDto>>
{
    public int ClienteId { get; set; }
}

internal sealed class GetSolicitudesCotizacionByUsuarioQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetSolicitudesCotizacionByUsuarioQuery, List<SolicitudCotizacionDto>>
{
    public async Task<List<SolicitudCotizacionDto>> Handle(
        GetSolicitudesCotizacionByUsuarioQuery request,
        CancellationToken cancellationToken)
    {
        var solicitudes = await unitOfWork.SolicitudCotizacionRepository.GetByClienteIdAsync(request.ClienteId);

        return solicitudes.Select(solicitud => new SolicitudCotizacionDto
        {
            Id = solicitud.Id,
            DisenoId = solicitud.DisenoId,
            ClienteId = solicitud.ClienteId,
            Estado = solicitud.Estado,
            Fecha = solicitud.Fecha
        }).ToList();
    }
}
