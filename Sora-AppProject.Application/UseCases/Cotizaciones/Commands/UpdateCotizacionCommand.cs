using MediatR;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.Cotizaciones.Commands;

public class UpdateCotizacionCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public int SolicitudId { get; set; }
    public int TallerId { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Total { get; set; }
    public int PlazoDias { get; set; }
    public string Estado { get; set; } = null!;
}

internal sealed class UpdateCotizacionCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateCotizacionCommand, Unit>
{
    public async Task<Unit> Handle(UpdateCotizacionCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.CotizacionRepository.GetByIdAsync(request.Id)
                     ?? throw new KeyNotFoundException("Cotización no encontrada.");

        entity.SolicitudId = request.SolicitudId;
        entity.TallerId = request.TallerId;
        entity.PrecioUnitario = request.PrecioUnitario;
        entity.Total = request.Total;
        entity.PlazoDias = request.PlazoDias;
        entity.Estado = request.Estado;

        await unitOfWork.CotizacionRepository.UpdateAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return Unit.Value;
    }
}