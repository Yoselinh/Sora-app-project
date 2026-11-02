using MediatR;
using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Application.UseCases.PrendasBase.Commands;

public record AddPrendaBaseCommand(string Nombre, string? Descripcion) : IRequest<int>;

public class AddPrendaBaseCommandHandler : IRequestHandler<AddPrendaBaseCommand, int>
{
    private readonly ApplicationDbContext _context;
    public AddPrendaBaseCommandHandler(ApplicationDbContext context) => _context = context;

    public async Task<int> Handle(AddPrendaBaseCommand request, CancellationToken cancellationToken)
    {
        var item = new Sora_AppProject.Domain.Models.PrendasBase { Nombre = request.Nombre, Descripcion = request.Descripcion };
        await _context.PrendasBases.AddAsync(item, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return item.Id;
    }
}
