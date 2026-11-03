using MediatR;
using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Application.UseCases.DisenoTelas.Commands;

public record UpdateDisenoTelaCommand(
    int Id,
    int DisenoId,
    int TelaId,
    string Parte
) : IRequest<Unit>;

internal sealed class UpdateDisenoTelaCommandHandler
    : IRequestHandler<UpdateDisenoTelaCommand, Unit>
{
    private readonly ApplicationDbContext _context;

    public UpdateDisenoTelaCommandHandler(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(
        UpdateDisenoTelaCommand request,
        CancellationToken cancellationToken)
    {
        var disenoTela = await _context.DisenoTelas.FindAsync(
            new object[] { request.Id },
            cancellationToken
        );

        if (disenoTela is null)
            throw new KeyNotFoundException("DiseñoTela no encontrado.");

        disenoTela.DisenoId = request.DisenoId;
        disenoTela.TelaId = request.TelaId;
        disenoTela.Parte = request.Parte;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}