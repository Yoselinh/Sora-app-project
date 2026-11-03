using MediatR;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Infrastructure.Context;

namespace Sora_AppProject.Application.UseCases.Cotizaciones.Commands;

public record AddCotizacionCommand(int SolicitudId, decimal Total) : IRequest<int>;

public class AddCotizacionCommandHandler : IRequestHandler<AddCotigitzacionCommand, int>
{
    private readonly ApplicationDbContext _context;
    public AddCotizacionCommandHandler(ApplicationDbContext context) => _context = context;

    public async Task<int> Handle(AddCotizacionCommand request, CancellationToken cancellationToken)
    {
        var item = new Sora_AppProject.Domain.Models.Cotizacione { SolicitudId = request.SolicitudId, Total = request.Total };
        await _context.Cotizaciones.AddAsync(item, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return item.Id;
    }
}
