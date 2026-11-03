using MediatR;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Infrastructure.Context;

namespace Sora_AppProject.Application.UseCases.PrendasBase.Commands;

public record DeletePrendaBaseCommand(int Id) : IRequest<bool>;

public class DeletePrendaBaseCommandHandler : IRequestHandler<DeletePrendaBaseCommand, bool>
{
    private readonly ApplicationDbContext _context;
    public DeletePrendaBaseCommandHandler(ApplicationDbContext context) => _context = context;

    public async Task<bool> Handle(DeletePrendaBaseCommand request, CancellationToken cancellationToken)
    {
        var item = await _context.PrendasBases.FindAsync(new object[] { request.Id }, cancellationToken);
        if (item == null) return false;
        _context.PrendasBases.Remove(item);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
