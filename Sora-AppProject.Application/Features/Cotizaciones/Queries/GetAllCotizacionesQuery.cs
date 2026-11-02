using MediatR;
using Microsoft.EntityFrameworkCore;
using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Application.Features.Cotizaciones.Queries;

public record GetAllCotizacionesQuery : IRequest<List<Cotizacione>>;

public class GetAllCotizacionesQueryHandler : IRequestHandler<GetAllCotizacionesQuery, List<Cotizacione>>
{
    private readonly ApplicationDbContext _context;

    public GetAllCotizacionesQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Cotizacione>> Handle(GetAllCotizacionesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Cotizaciones.ToListAsync(cancellationToken);
    }
}
