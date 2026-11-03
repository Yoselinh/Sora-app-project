using MediatR;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Infrastructure.Context;

namespace Sora_AppProject.Application.UseCases.Telas.Commands;

public record UpdateTelaCommand(int Id, string Nombre) : IRequest<bool>;

public class UpdateTelaCommandHandler : IRequestHandler<UpdateTelaCommand, bool>
{
    private readonly ApplicationDbContext _context;
    public UpdateTelaCommandHandler(ApplicationDbContext context) => _context = context;

    public async Task<bool> Handle(UpdateTelaCommand request, CancellationToken cancellationToken)
    {
        var item = await _context.Telas.FindAsync(new object[] { request.Id }, cancellationToken);
        if (item == null) return false;
        item.Nombre = request.Nombre;
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
