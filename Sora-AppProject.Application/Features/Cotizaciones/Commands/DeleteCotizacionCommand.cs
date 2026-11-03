using MediatR;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Infrastructure.Context;

namespace Sora_AppProject.Application.Features.Cotizaciones.Commands;

public record DeleteCotizacionCommand(int Id) : IRequest<bool>;

public class DeleteCotizacionCommandHandler : IRequestHandler<DeleteCotizacionCommand, bool>
{
    private readonly ApplicationDbContext _context;
    public DeleteCotizacionCommandHandler(ApplicationDbContext context) => _context = context;

    public async Task<bool> Handle(DeleteCotizacionCommand request, CancellationToken cancellationToken)
    {
        var item = await _context.Cotizaciones.FindAsync(new object[] { request.Id }, cancellationToken);
        if (item == null) return false;
        _context.Cotizaciones.Remove(item);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
