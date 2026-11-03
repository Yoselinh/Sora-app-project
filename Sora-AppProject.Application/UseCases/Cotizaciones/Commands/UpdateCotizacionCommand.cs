using MediatR;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Infrastructure.Context;

namespace Sora_AppProject.Application.UseCases.Cotizaciones.Commands;

public record UpdateCotizacionCommand(int Id, int SolicitudId, decimal Total) : IRequest<bool>;

public class UpdateCotizacionCommandHandler : IRequestHandler<UpdateCotizacionCommand, bool>
{
    private readonly ApplicationDbContext _context;
    public UpdateCotizacionCommandHandler(ApplicationDbContext context) => _context = context;

    public async Task<bool> Handle(UpdateCotizacionCommand request, CancellationToken cancellationToken)
    {
        var item = await _context.Cotizaciones.FindAsync(new object[] { request.Id }, cancellationToken);
        if (item == null) return false;
        item.SolicitudId = request.SolicitudId;
        item.Total = request.Total;
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
