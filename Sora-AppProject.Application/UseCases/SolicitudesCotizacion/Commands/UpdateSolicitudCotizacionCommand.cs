using MediatR;
using Sora_AppProject.Domain.Repositories;

namespace Sora_AppProject.Application.UseCases.SolicitudesCotizacion.Commands;

public sealed record UpdateSolicitudCotizacionCommand(int Id, string Estado) : IRequest<bool>;

public sealed class UpdateSolicitudCotizacionCommandHandler
    : IRequestHandler<UpdateSolicitudCotizacionCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSolicitudCotizacionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(
        UpdateSolicitudCotizacionCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.SolicitudCotizacionRepository;
        var solicitud = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (solicitud is null)
        {
            return false;
        }

        solicitud.Estado = request.Estado;
        await repository.UpdateAsync(solicitud, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
