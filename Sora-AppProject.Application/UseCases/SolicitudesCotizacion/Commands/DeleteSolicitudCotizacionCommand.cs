using MediatR;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.SolicitudesCotizacion.Commands;

public class DeleteSolicitudCotizacionCommand : IRequest<Unit>
{
    public int Id { get; set; }
}

internal sealed class DeleteSolicitudCotizacionCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteSolicitudCotizacionCommand, Unit>
{
    public async Task<Unit> Handle(DeleteSolicitudCotizacionCommand request, CancellationToken cancellationToken)
    {
        var solicitud = await unitOfWork.SolicitudCotizacionRepository.GetByIdAsync(request.Id)
                        ?? throw new KeyNotFoundException("Solicitud de cotización no encontrada.");

        if (await unitOfWork.SolicitudCotizacionRepository.HasCotizacionesAsync(request.Id))
        {
            throw new InvalidOperationException(
                "No se puede eliminar la solicitud porque ya tiene cotizaciones registradas.");
        }

        await unitOfWork.SolicitudCotizacionRepository.DeleteAsync(solicitud);
        await unitOfWork.SaveChangesAsync();
        return Unit.Value;
    }
}
