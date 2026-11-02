using MediatR;
using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Application.UseCases.Disenos.Commands;

public record CreateDisenoCommand(
    int ClienteId,
    int PrendaBaseId,
    string Nombre,
    string ConfiguracionVisor
) : IRequest<int>;

internal sealed class CreateDisenoCommandHandler
    : IRequestHandler<CreateDisenoCommand, int>
{
    private readonly ApplicationDbContext _context;

    public CreateDisenoCommandHandler(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(
        CreateDisenoCommand request,
        CancellationToken cancellationToken)
    {
        var diseno = new Diseno
        {
            ClienteId = request.ClienteId,
            PrendaBaseId = request.PrendaBaseId,
            Nombre = request.Nombre,
            ConfiguracionVisor = request.ConfiguracionVisor
        };

        await _context.Disenos.AddAsync(
            diseno,
            cancellationToken
        );

        await _context.SaveChangesAsync(
            cancellationToken
        );

        return diseno.Id;
    }
}