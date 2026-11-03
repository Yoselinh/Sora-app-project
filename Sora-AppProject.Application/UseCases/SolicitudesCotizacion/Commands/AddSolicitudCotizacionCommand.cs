using MediatR;
using Sora_AppProject.Domain.Repositories;
using SolicitudCotizacionEntity = Sora_AppProject.Domain.Models.SolicitudesCotizacion;

namespace Sora_AppProject.Application.UseCases.SolicitudesCotizacion.Commands;

public sealed record AddSolicitudCotizacionCommand(int ClienteId, int DisenoId) : IRequest<int>;

public sealed class AddSolicitudCotizacionCommandHandler
    : IRequestHandler<AddSolicitudCotizacionCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddSolicitudCotizacionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(
        AddSolicitudCotizacionCommand request,
        CancellationToken cancellationToken)
    {
        var solicitud = new SolicitudCotizacionEntity
        {
            ClienteId = request.ClienteId,
            DisenoId = request.DisenoId,
            Estado = "pendiente"
        };

        await _unitOfWork.SolicitudCotizacionRepository.AddAsync(solicitud, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return solicitud.Id;
    }
}
