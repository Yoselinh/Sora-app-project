using MediatR;
using Sora_AppProject.Application.Dtos;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.SolicitudesCotizacion.Queries;

public class GetSolicitudCotizacionByIdQuery : IRequest<SolicitudCotizacionDto>
{
    public int Id { get; set; }
}

internal sealed class GetSolicitudCotizacionByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetSolicitudCotizacionByIdQuery, SolicitudCotizacionDto>
{
    public async Task<SolicitudCotizacionDto> Handle(
        GetSolicitudCotizacionByIdQuery request,
        CancellationToken cancellationToken)
    {
        var solicitud = await unitOfWork.SolicitudCotizacionRepository.GetByIdAsync(request.Id)
                        ?? throw new KeyNotFoundException("Solicitud de cotización no encontrada.");

        return new SolicitudCotizacionDto
        {
            Id = solicitud.Id,
            DisenoId = solicitud.DisenoId,
            ClienteId = solicitud.ClienteId,
            Estado = solicitud.Estado,
            Fecha = solicitud.Fecha
        };
    }
}
