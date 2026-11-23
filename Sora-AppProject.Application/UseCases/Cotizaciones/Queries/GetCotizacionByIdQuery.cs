using MediatR;
using Sora_AppProject.Application.Dtos;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.Cotizaciones.Queries;

public class GetCotizacionByIdQuery : IRequest<CotizacionDto>
{
    public int Id { get; set; }

    public GetCotizacionByIdQuery(int id)
    {
        Id = id;
    }
}

internal sealed class GetCotizacionByIdQueryHandler : IRequestHandler<GetCotizacionByIdQuery, CotizacionDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCotizacionByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CotizacionDto> Handle(GetCotizacionByIdQuery query, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.CotizacionRepository.GetByIdAsync(query.Id);

        if (entity is null)
        {
            throw new KeyNotFoundException("Cotización no encontrada.");
        }

        return new CotizacionDto
        {
            Id = entity.Id,
            SolicitudId = entity.SolicitudId,
            TallerId = entity.TallerId,
            PrecioUnitario = entity.PrecioUnitario,
            Total = entity.Total,
            PlazoDias = entity.PlazoDias,
            Estado = entity.Estado,
            Fecha = entity.Fecha
        };
    }
}
