using MediatR;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Infrastructure.Context;

namespace Sora_AppProject.Application.UseCases.DisenoTelas.Commands;

public record DeleteDisenoTelaCommand(
    int Id
) : IRequest<Unit>;

internal sealed class DeleteDisenoTelaCommandHandler
    : IRequestHandler<DeleteDisenoTelaCommand, Unit>
{
    private readonly ApplicationDbContext _context;

    public DeleteDisenoTelaCommandHandler(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(
        DeleteDisenoTelaCommand request,
        CancellationToken cancellationToken)
    {
        var disenoTela = await _context.DisenoTelas.FindAsync(
            new object[] { request.Id },
            cancellationToken
        );

        if (disenoTela is null)
            throw new KeyNotFoundException("DiseñoTela no encontrado.");

        _context.DisenoTelas.Remove(disenoTela);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}