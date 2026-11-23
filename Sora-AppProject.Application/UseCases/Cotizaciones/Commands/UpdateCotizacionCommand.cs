using MediatR;
using Sora_AppProject.Application.Dtos;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.Cotizaciones.Commands;

public class UpdateCotizacionCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public CotizacionDto Request { get; set; }

    public UpdateCotizacionCommand(int id, CotizacionDto request)
    {
        Id = id;
        Request = request;
    }
}

internal sealed class UpdateCotizacionCommandHandler : IRequestHandler<UpdateCotizacionCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCotizacionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateCotizacionCommand command, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.CotizacionRepository.GetByIdAsync(command.Id);

        if (entity is null)
        {
            throw new KeyNotFoundException("Cotización no encontrada.");
        }

        entity.SolicitudId = command.Request.SolicitudId;
        entity.TallerId = command.Request.TallerId;
        entity.PrecioUnitario = command.Request.PrecioUnitario;
        entity.Total = command.Request.Total;
        entity.PlazoDias = command.Request.PlazoDias;
        entity.Estado = command.Request.Estado;

        await _unitOfWork.CotizacionRepository.UpdateAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}
