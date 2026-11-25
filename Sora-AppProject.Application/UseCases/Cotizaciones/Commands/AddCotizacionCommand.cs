using MediatR;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.Cotizaciones.Commands;

public class AddCotizacionCommand : IRequest<Unit>
{
    public int SolicitudId { get; set; }
    public int TallerId { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Total { get; set; }
    public int PlazoDias { get; set; }
}

internal sealed class AddCotizacionCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<AddCotizacionCommand, Unit>
{
    public async Task<Unit> Handle(AddCotizacionCommand request, CancellationToken cancellationToken)
    {
        if (request.PrecioUnitario <= 0 || request.Total <= 0)
            throw new ArgumentException("El precio unitario y el total deben ser mayores a cero.");

        if (request.PlazoDias <= 0)
            throw new ArgumentException("El plazo en días debe ser mayor a cero.");

        await unitOfWork.CotizacionRepository.AddAsync(new Cotizacione
        {
            SolicitudId = request.SolicitudId,
            TallerId = request.TallerId,
            PrecioUnitario = request.PrecioUnitario,
            Total = request.Total,
            PlazoDias = request.PlazoDias,
            Estado = "enviada",
            Fecha = DateTime.UtcNow
        });

        await unitOfWork.SaveChangesAsync();
        return Unit.Value;
    }
}