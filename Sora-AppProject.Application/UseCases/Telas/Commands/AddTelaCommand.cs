using MediatR;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Infrastructure.Context;

namespace Sora_AppProject.Application.UseCases.Telas.Commands;

public record AddTelaCommand(string Nombre) : IRequest<int>;

public class AddTelaCommandHandler : IRequestHandler<AddTelaCommand, int>
{
    private readonly ApplicationDbContext _context;
    public AddTelaCommandHandler(ApplicationDbContext context) => _context = context;

    public async Task<int> Handle(AddTelaCommand request, CancellationToken cancellationToken)
    {
        var item = new Sora_AppProject.Domain.Models.Tela { Nombre = request.Nombre };
        await _context.Telas.AddAsync(item, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return item.Id;
    }
}
