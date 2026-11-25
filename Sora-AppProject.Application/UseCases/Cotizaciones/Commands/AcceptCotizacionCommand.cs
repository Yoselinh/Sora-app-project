using MediatR;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.Cotizaciones.Commands;


public record AcceptCotizacionCommand(int CotizacionId, int ClienteId) : IRequest<Unit>;

internal sealed class AcceptCotizacionCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<AcceptCotizacionCommand, Unit>
{
    
    public async Task<Unit> Handle(AcceptCotizacionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var cotizacion = await unitOfWork.CotizacionRepository.GetByIdAsync(request.CotizacionId)
                             ?? throw new KeyNotFoundException("Cotización no encontrada.");

            var solicitud = await unitOfWork.SolicitudCotizacionRepository.GetByIdAsync(cotizacion.SolicitudId)
                            ?? throw new KeyNotFoundException("Solicitud no encontrada.");

            if (solicitud.ClienteId != request.ClienteId)
                throw new UnauthorizedAccessException("Esta solicitud no te pertenece.");

            if (cotizacion.Estado != "enviada" || solicitud.Estado == "aceptada")
                throw new InvalidOperationException("Esta cotización ya no se puede aceptar.");

            cotizacion.Estado = "aceptada";
            solicitud.Estado = "aceptada";

            await unitOfWork.CotizacionRepository.UpdateAsync(cotizacion);
            await unitOfWork.SolicitudCotizacionRepository.UpdateAsync(solicitud);

            await unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR AL ACEPTAR COTIZACIÓN:");
            Console.WriteLine(ex.ToString());
            throw;
        }
    }
    
}

