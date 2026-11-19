using MediatR;
using Sora_AppProject.Domain.Ports.Repositories;
using SolicitudCotizacionEntity = Sora_AppProject.Domain.Models.SolicitudesCotizacion;

namespace Sora_AppProject.Application.UseCases.SolicitudesCotizacion.Queries;

public sealed record GetAllSolicitudesCotizacionQuery : IRequest<IReadOnlyList<SolicitudCotizacionEntity>>;

public sealed class GetAllSolicitudesCotizacionQueryHandler
    : IRequestHandler<GetAllSolicitudesCotizacionQuery, IReadOnlyList<SolicitudCotizacionEntity>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllSolicitudesCotizacionQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<IReadOnlyList<SolicitudCotizacionEntity>> Handle(
        GetAllSolicitudesCotizacionQuery request,
        CancellationToken cancellationToken) =>
        _unitOfWork.SolicitudCotizacionRepository.GetAllAsync(cancellationToken);
}
