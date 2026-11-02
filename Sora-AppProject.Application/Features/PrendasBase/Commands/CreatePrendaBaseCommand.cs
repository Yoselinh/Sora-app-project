using MediatR;
using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Application.Features.PrendasBase.Commands;

public record CreatePrendaBaseCommand(string Nombre, string? Descripcion) : IRequest<int>;

public class CreatePrendaBaseCommandHandler : IRequestHandler<CreatePrendaBaseCommand, int>
{
    private readonly ApplicationDbContext _context;

    public CreatePrendaBaseCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreatePrendaBaseCommand request, CancellationToken cancellationToken)
    {
        var prenda = new Sora_AppProject.Domain.Models.PrendasBase
        {
            Nombre = request.Nombre,
            Descripcion = request.Descripcion
        };

        _context.PrendasBases.Add(prenda);
        await _context.SaveChangesAsync(cancellationToken);

        return prenda.Id;
    }
}
