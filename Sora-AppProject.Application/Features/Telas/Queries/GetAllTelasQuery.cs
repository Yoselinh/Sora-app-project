using MediatR;
using Microsoft.EntityFrameworkCore;
using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Application.Features.Telas.Queries;

public record GetAllTelasQuery : IRequest<List<Tela>>;

public class GetAllTelasQueryHandler : IRequestHandler<GetAllTelasQuery, List<Tela>>
{
    private readonly ApplicationDbContext _context;

    public GetAllTelasQueryHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Tela>> Handle(GetAllTelasQuery request, CancellationToken cancellationToken)
    {
        return await _context.Telas.ToListAsync(cancellationToken);
    }
}
