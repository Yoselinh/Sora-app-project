using MediatR;
using Microsoft.EntityFrameworkCore;
using Sora_AppProject.Domain.Models;
 
namespace Sora_AppProject.Application.UseCases.Talleres.Commands;
 
public class DeleteTallerCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
 
internal sealed class DeleteTallerCommandHandler
    : IRequestHandler<DeleteTallerCommand, Unit>
{
    private readonly ApplicationDbContext _context;
 
    public DeleteTallerCommandHandler(
        ApplicationDbContext context)
    {
        _context = context;
    }
 
    public async Task<Unit> Handle(
        DeleteTallerCommand request,
        CancellationToken cancellationToken)
    {
        var taller = await _context.Talleres.FindAsync(
            new object[] { request.Id },
            cancellationToken
        );
 
        if (taller is null)
            throw new KeyNotFoundException("Taller no encontrado.");
 
        var tieneCotizaciones = await _context.Cotizaciones
            .AnyAsync(c => c.TallerId == request.Id, cancellationToken);
 
        if (tieneCotizaciones)
            throw new InvalidOperationException("No se puede eliminar el taller porque tiene cotizaciones registradas.");
 
        var telasDelTaller = await _context.TallerTelas
            .Where(tt => tt.TallerId == request.Id)
            .ToListAsync(cancellationToken);
 
        _context.TallerTelas.RemoveRange(telasDelTaller);
        _context.Talleres.Remove(taller);
 
        await _context.SaveChangesAsync(cancellationToken);
 
        return Unit.Value;
    }
}