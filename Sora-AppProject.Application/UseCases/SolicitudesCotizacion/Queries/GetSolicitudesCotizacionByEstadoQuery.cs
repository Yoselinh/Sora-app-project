using MediatR;
using SolicitudCotizacionEntity = Sora_AppProject.Domain.Models.SolicitudesCotizacion;
using Sora_AppProject.Domain.Repositories;

namespace Sora_AppProject.Application.UseCases.SolicitudesCotizacion.Queries;

public sealed record GetSolicitudesCotizacionByEstadoQuery(string Estado)
    : IRequest<IReadOnlyList<SolicitudCotizacionEntity>>;

public sealed class GetSolicitudesCotizacionByEstadoQueryHandler
    : IRequestHandler<GetSolicitudesCotizacionByEstadoQuery, IReadOnlyList<SolicitudCotizacionEntity>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSolicitudesCotizacionByEstadoQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<IReadOnlyList<SolicitudCotizacionEntity>> Handle(
        GetSolicitudesCotizacionByEstadoQuery request,
        CancellationToken cancellationToken) =>
        _unitOfWork.SolicitudCotizacionRepository.GetByEstadoAsync(request.Estado, cancellationToken);
}
