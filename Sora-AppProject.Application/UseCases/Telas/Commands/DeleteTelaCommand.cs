using MediatR;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Infrastructure.Context;

namespace Sora_AppProject.Application.UseCases.Telas.Commands;

public record DeleteTelaCommand(int Id) : IRequest<bool>;

public class DeleteTelaCommandHandler : IRequestHandler<DeleteTelaCommand, bool>
{
    private readonly ApplicationDbContext _context;
    public DeleteTelaCommandHandler(ApplicationDbContext context) => _context = context;

    public async Task<bool> Handle(DeleteTelaCommand request, CancellationToken cancellationToken)
    {
        var item = await _context.Telas.FindAsync(new object[] { request.Id }, cancellationToken);
        if (item == null) return false;
        _context.Telas.Remove(item);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
