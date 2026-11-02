using MediatR;
using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Application.UseCases.Disenos.Commands;

public record DeleteDisenoCommand(
    int Id
) : IRequest<Unit>;

internal sealed class DeleteDisenoCommandHandler
    : IRequestHandler<DeleteDisenoCommand, Unit>
{
    private readonly ApplicationDbContext _context;

    public DeleteDisenoCommandHandler(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(
        DeleteDisenoCommand request,
        CancellationToken cancellationToken)
    {
        var diseno = await _context.Disenos.FindAsync(
            new object[] { request.Id },
            cancellationToken
        );

        if (diseno is null)
            throw new KeyNotFoundException("Diseño no encontrado.");

        _context.Disenos.Remove(diseno);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}