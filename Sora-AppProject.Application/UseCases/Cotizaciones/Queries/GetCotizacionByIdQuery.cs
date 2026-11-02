using MediatR;
using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Application.UseCases.Cotizaciones.Queries;

public record GetCotizacionByIdQuery(int Id) : IRequest<Sora_AppProject.Domain.Models.Cotizacione?>;

public class GetCotizacionByIdQueryHandler : IRequestHandler<GetCotizacionByIdQuery, Sora_AppProject.Domain.Models.Cotizacione?>
{
    private readonly ApplicationDbContext _context;
    public GetCotizacionByIdQueryHandler(ApplicationDbContext context) => _context = context;

    public async Task<Sora_AppProject.Domain.Models.Cotizacione?> Handle(GetCotizacionByIdQuery request, CancellationToken cancellationToken)
        => await _context.Cotizaciones.FindAsync(new object[] { request.Id }, cancellationToken);
}
