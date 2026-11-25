using MediatR;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.Cotizaciones.Commands;

public record RejectCotizacionCommand(int CotizacionId, int ClienteId) : IRequest<Unit>;

internal sealed class RejectCotizacionCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<RejectCotizacionCommand, Unit>
{
    public async Task<Unit> Handle(RejectCotizacionCommand request, CancellationToken cancellationToken)
    {
        var cotizacion = await unitOfWork.CotizacionRepository.GetByIdAsync(request.CotizacionId)
                         ?? throw new KeyNotFoundException("Cotización no encontrada.");

        var solicitud = await unitOfWork.SolicitudCotizacionRepository.GetByIdAsync(cotizacion.SolicitudId)
                        ?? throw new KeyNotFoundException("Solicitud no encontrada.");

        if (solicitud.ClienteId != request.ClienteId)
            throw new UnauthorizedAccessException("Esta solicitud no te pertenece.");

        if (cotizacion.Estado != "enviada" || solicitud.Estado == "aceptada")
            throw new InvalidOperationException("Esta cotización ya no se puede rechazar.");

        cotizacion.Estado = "rechazada";

        await unitOfWork.CotizacionRepository.UpdateAsync(cotizacion);
        await unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}
