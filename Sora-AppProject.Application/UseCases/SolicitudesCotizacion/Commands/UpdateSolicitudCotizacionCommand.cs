using MediatR;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.SolicitudesCotizacion.Commands;

public class UpdateSolicitudCotizacionCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string Estado { get; set; } = null!;
}

internal sealed class UpdateSolicitudCotizacionCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateSolicitudCotizacionCommand, Unit>
{
    public async Task<Unit> Handle(UpdateSolicitudCotizacionCommand request, CancellationToken cancellationToken)
    {
        var solicitud = await unitOfWork.SolicitudCotizacionRepository.GetByIdAsync(request.Id)
                        ?? throw new KeyNotFoundException("Solicitud de cotización no encontrada.");

        solicitud.Estado = request.Estado;
        await unitOfWork.SaveChangesAsync();
        return Unit.Value;
    }
}
