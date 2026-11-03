using MediatR;
using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Application.UseCases.Disenos.Commands;

public record UpdateDisenoCommand(
    int Id,
    int ClienteId,
    int PrendaBaseId,
    string Nombre,
    string ConfiguracionVisor
) : IRequest<Unit>;

internal sealed class UpdateDisenoCommandHandler
    : IRequestHandler<UpdateDisenoCommand, Unit>
{
    private readonly ApplicationDbContext _context;

    public UpdateDisenoCommandHandler(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(
        UpdateDisenoCommand request,
        CancellationToken cancellationToken)
    {
        var diseno = await _context.Disenos.FindAsync(
            new object[] { request.Id },
            cancellationToken
        );

        if (diseno is null)
            throw new KeyNotFoundException("Diseño no encontrado.");

        diseno.ClienteId = request.ClienteId;
        diseno.PrendaBaseId = request.PrendaBaseId;
        diseno.Nombre = request.Nombre;
        diseno.ConfiguracionVisor = request.ConfiguracionVisor;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}