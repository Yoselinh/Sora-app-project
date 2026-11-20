using MediatR;
using Sora_AppProject.Domain.Ports.Repositories;
using SolicitudCotizacionEntity = Sora_AppProject.Domain.Models.SolicitudesCotizacion;

namespace Sora_AppProject.Application.UseCases.SolicitudesCotizacion.Queries;

public sealed record GetSolicitudCotizacionByIdQuery(int Id) : IRequest<SolicitudCotizacionEntity?>;

public sealed class GetSolicitudCotizacionByIdQueryHandler
    : IRequestHandler<GetSolicitudCotizacionByIdQuery, SolicitudCotizacionEntity?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSolicitudCotizacionByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<SolicitudCotizacionEntity?> Handle(
        GetSolicitudCotizacionByIdQuery request,
        CancellationToken cancellationToken) =>
        _unitOfWork.SolicitudCotizacionRepository.GetByIdAsync(request.Id, cancellationToken);
}
