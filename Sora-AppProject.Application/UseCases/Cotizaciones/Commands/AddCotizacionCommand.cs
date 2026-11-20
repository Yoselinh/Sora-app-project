using MediatR;
using Sora_AppProject.Application.Dtos.Cotizaciones;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.Cotizaciones.Commands;

public class AddCotizacionCommand : IRequest<int>
{
    public CreateCotizacionRequestDto Request { get; set; }

    public AddCotizacionCommand(CreateCotizacionRequestDto request)
    {
        Request = request;
    }
}

internal sealed class AddCotizacionCommandHandler : IRequestHandler<AddCotizacionCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddCotizacionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(AddCotizacionCommand command, CancellationToken cancellationToken)
    {
        var entity = new Cotizacione
        {
            SolicitudId = command.Request.SolicitudId,
            TallerId = command.Request.TallerId,
            PrecioUnitario = command.Request.PrecioUnitario,
            Total = command.Request.Total,
            PlazoDias = command.Request.PlazoDias,
            Estado = command.Request.Estado,
            Fecha = DateTime.UtcNow
        };

        await _unitOfWork.CotizacionRepository.AddAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        return entity.Id;
    }
}
