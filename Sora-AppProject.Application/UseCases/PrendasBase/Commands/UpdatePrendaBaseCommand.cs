using MediatR;
using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Application.UseCases.PrendasBase.Commands;

public record UpdatePrendaBaseCommand(int Id, string Nombre, string? Descripcion) : IRequest<bool>;

public class UpdatePrendaBaseCommandHandler : IRequestHandler<UpdatePrendaBaseCommand, bool>
{
    private readonly ApplicationDbContext _context;
    public UpdatePrendaBaseCommandHandler(ApplicationDbContext context) => _context = context;

    public async Task<bool> Handle(UpdatePrendaBaseCommand request, CancellationToken cancellationToken)
    {
        var item = await _context.PrendasBases.FindAsync(new object[] { request.Id }, cancellationToken);
        if (item == null) return false;
        item.Nombre = request.Nombre;
        item.Descripcion = request.Descripcion;
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
