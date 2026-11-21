using MediatR;
using Sora_AppProject.Application.Dtos;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.SolicitudesCotizacion.Queries;

public class GetSolicitudesCotizacionByEstadoQuery : IRequest<List<SolicitudCotizacionDto>>
{
    public string Estado { get; set; } = null!;
}

internal sealed class GetSolicitudesCotizacionByEstadoQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetSolicitudesCotizacionByEstadoQuery, List<SolicitudCotizacionDto>>
{
    public async Task<List<SolicitudCotizacionDto>> Handle(
        GetSolicitudesCotizacionByEstadoQuery request,
        CancellationToken cancellationToken)
    {
        var solicitudes = await unitOfWork.SolicitudCotizacionRepository.GetByEstadoAsync(request.Estado);

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
