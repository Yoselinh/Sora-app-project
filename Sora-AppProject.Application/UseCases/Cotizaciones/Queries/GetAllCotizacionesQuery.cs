using MediatR;
using Microsoft.EntityFrameworkCore;
using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Application.UseCases.Cotizaciones.Queries;

public record GetAllCotizacionesQuery : IRequest<List<Sora_AppProject.Domain.Models.Cotizacione>>;

public class GetAllCotizacionesQueryHandler : IRequestHandler<GetAllCotizacionesQuery, List<Sora_AppProject.Domain.Models.Cotizacione>>
{
    private readonly ApplicationDbContext _context;
    public GetAllCotizacionesQueryHandler(ApplicationDbContext context) => _context = context;

    public async Task<List<Sora_AppProject.Domain.Models.Cotizacione>> Handle(GetAllCotizacionesQuery request, CancellationToken cancellationToken)
        => await _context.Cotizaciones.ToListAsync(cancellationToken);
}
