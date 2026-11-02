using MediatR;
using Sora_AppProject.Domain.Models;
 
namespace Sora_AppProject.Application.UseCases.Talleres.Commands;
 
public class UpdateTallerCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Direccion { get; set; }
    public string? Telefono { get; set; }
}
 
internal sealed class UpdateTallerCommandHandler
    : IRequestHandler<UpdateTallerCommand, Unit>
{
    private readonly ApplicationDbContext _context;
 
    public UpdateTallerCommandHandler(
        ApplicationDbContext context)
    {
        _context = context;
    }
 
    public async Task<Unit> Handle(
        UpdateTallerCommand request,
        CancellationToken cancellationToken)
    {
        var taller = await _context.Talleres.FindAsync(
            new object[] { request.Id },
            cancellationToken
        );
 
        if (taller is null)
            throw new KeyNotFoundException("Taller no encontrado.");
 
        taller.Nombre = request.Nombre;
        taller.Direccion = request.Direccion;
        taller.Telefono = request.Telefono;
 
        await _context.SaveChangesAsync(cancellationToken);
 
        return Unit.Value;
    }
}