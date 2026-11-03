using MediatR;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Infrastructure.Context;

namespace Sora_AppProject.Application.UseCases.Disenos.Commands;

public record AddDisenoCommand(
    int ClienteId,
    int PrendaBaseId,
    string Nombre,
    string ConfiguracionVisor
) : IRequest<int>;

internal sealed class AddDisenoCommandHandler
    : IRequestHandler<AddDisenoCommand, int>
{
    private readonly ApplicationDbContext _context;

    public AddDisenoCommandHandler(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(
        AddDisenoCommand request,
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