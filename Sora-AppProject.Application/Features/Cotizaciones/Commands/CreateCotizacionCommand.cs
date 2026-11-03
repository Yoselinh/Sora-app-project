using MediatR;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Infrastructure.Context;

namespace Sora_AppProject.Application.Features.Cotizaciones.Commands;

public record CreateCotizacionCommand(int SolicitudId, decimal Total) : IRequest<int>;

public class CreateCotizacionCommandHandler : IRequestHandler<CreateCotizacionCommand, int>
{
    private readonly ApplicationDbContext _context;
    public CreateCotizacionCommandHandler(ApplicationDbContext context) => _context = context;

    public async Task<int> Handle(CreateCotizacionCommand request, CancellationToken cancellationToken)
    {
        var cotizacion = new Sora_AppProject.Domain.Models.Cotizacione
        {
            SolicitudId = request.SolicitudId,
            Total = request.Total
        };
        _context.Cotizaciones.Add(cotizacion);
        await _context.SaveChangesAsync(cancellationToken);
        return cotizacion.Id;
    }
}
