using MediatR;
using SolicitudCotizacionEntity = Sora_AppProject.Domain.Models.SolicitudesCotizacion;
using Sora_AppProject.Domain.Repositories;

namespace Sora_AppProject.Application.UseCases.SolicitudesCotizacion.Queries;

public sealed record GetSolicitudesCotizacionByUsuarioQuery(int ClienteId)
    : IRequest<IReadOnlyList<SolicitudCotizacionEntity>>;

public sealed class GetSolicitudesCotizacionByUsuarioQueryHandler
    : IRequestHandler<GetSolicitudesCotizacionByUsuarioQuery, IReadOnlyList<SolicitudCotizacionEntity>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSolicitudesCotizacionByUsuarioQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<IReadOnlyList<SolicitudCotizacionEntity>> Handle(
        GetSolicitudesCotizacionByUsuarioQuery request,
        CancellationToken cancellationToken) =>
        _unitOfWork.SolicitudCotizacionRepository.GetByClienteIdAsync(request.ClienteId, cancellationToken);
}
