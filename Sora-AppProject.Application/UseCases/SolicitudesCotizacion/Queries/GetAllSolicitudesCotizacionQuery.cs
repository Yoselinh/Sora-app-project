using MediatR;
using Sora_AppProject.Application.Dtos;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.SolicitudesCotizacion.Queries;

public class GetAllSolicitudesCotizacionQuery : IRequest<List<SolicitudCotizacionDto>>
{
}

internal sealed class GetAllSolicitudesCotizacionQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllSolicitudesCotizacionQuery, List<SolicitudCotizacionDto>>
{
    public async Task<List<SolicitudCotizacionDto>> Handle(
        GetAllSolicitudesCotizacionQuery request,
        CancellationToken cancellationToken)
    {
        var solicitudes = await unitOfWork.SolicitudCotizacionRepository.GetAllAsync();

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
