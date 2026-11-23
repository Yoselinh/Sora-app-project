using MediatR;
using Sora_AppProject.Application.Dtos.Common;
using Sora_AppProject.Application.Dtos;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.Cotizaciones.Queries;

public class GetAllCotizacionesQuery : IRequest<PaginatedDto<CotizacionDto>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public GetAllCotizacionesQuery(int pageNumber = 1, int pageSize = 10)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}

internal sealed class GetAllCotizacionesQueryHandler : IRequestHandler<GetAllCotizacionesQuery, PaginatedDto<CotizacionDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllCotizacionesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginatedDto<CotizacionDto>> Handle(GetAllCotizacionesQuery query, CancellationToken cancellationToken)
    {
        var allItems = await _unitOfWork.CotizacionRepository.GetAllAsync();

        var totalCount = allItems.Count;
        var pagedItems = allItems
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(c => new CotizacionDto
            {
                Id = c.Id,
                SolicitudId = c.SolicitudId,
                TallerId = c.TallerId,
                PrecioUnitario = c.PrecioUnitario,
                Total = c.Total,
                PlazoDias = c.PlazoDias,
                Estado = c.Estado,
                Fecha = c.Fecha
            })
            .ToList();

        return new PaginatedDto<CotizacionDto>
        {
            Items = pagedItems,
            TotalCount = totalCount,
            PageNumber = query.PageNumber,
            PageSize = query.PageSize
        };
    }
}
