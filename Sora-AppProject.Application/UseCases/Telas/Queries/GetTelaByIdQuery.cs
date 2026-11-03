using MediatR;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Infrastructure.Context;

namespace Sora_AppProject.Application.UseCases.Telas.Queries;

public record GetTelaByIdQuery(int Id) : IRequest<Sora_AppProject.Domain.Models.Tela?>;

public class GetTelaByIdQueryHandler : IRequestHandler<GetTelaByIdQuery, Sora_AppProject.Domain.Models.Tela?>
{
    private readonly ApplicationDbContext _context;
    public GetTelaByIdQueryHandler(ApplicationDbContext context) => _context = context;

    public async Task<Sora_AppProject.Domain.Models.Tela?> Handle(GetTelaByIdQuery request, CancellationToken cancellationToken)
        => await _context.Telas.FindAsync(new object[] { request.Id }, cancellationToken);
}
