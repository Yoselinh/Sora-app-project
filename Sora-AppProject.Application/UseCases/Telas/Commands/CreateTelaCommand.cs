using MediatR;
using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Application.UseCases.Telas.Commands;

public record CreateTelaCommand(string Nombre, string? Descripcion) : IRequest<int>;

public class CreateTelaCommandHandler : IRequestHandler<CreateTelaCommand, int>
{
    private readonly ApplicationDbContext _context;
    public CreateTelaCommandHandler(ApplicationDbContext context) => _context = context;

    public async Task<int> Handle(CreateTelaCommand request, CancellationToken cancellationToken)
    {
        var tela = new Sora_AppProject.Domain.Models.Tela
        {
            Nombre = request.Nombre,
            Descripcion = request.Descripcion
        };
        _context.Telas.Add(tela);
        await _context.SaveChangesAsync(cancellationToken);
        return tela.Id;
    }
}
