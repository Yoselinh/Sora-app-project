using MediatR;
using Microsoft.EntityFrameworkCore;
using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Application.UseCases.Telas.Queries;

public record GetAllTelasQuery : IRequest<List<Sora_AppProject.Domain.Models.Tela>>;

public class GetAllTelasQueryHandler : IRequestHandler<GetAllTelasQuery, List<Sora_AppProject.Domain.Models.Tela>>
{
    private readonly ApplicationDbContext _context;
    public GetAllTelasQueryHandler(ApplicationDbContext context) => _context = context;

    public async Task<List<Sora_AppProject.Domain.Models.Tela>> Handle(GetAllTelasQuery request, CancellationToken cancellationToken)
        => await _context.Telas.ToListAsync(cancellationToken);
}
