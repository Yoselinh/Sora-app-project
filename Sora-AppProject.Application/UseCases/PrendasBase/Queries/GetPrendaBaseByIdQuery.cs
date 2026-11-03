using MediatR;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Infrastructure.Context;

namespace Sora_AppProject.Application.UseCases.PrendasBase.Queries;

public record GetPrendaBaseByIdQuery(int Id) : IRequest<Sora_AppProject.Domain.Models.PrendasBase?>;

public class GetPrendaBaseByIdQueryHandler : IRequestHandler<GetPrendaBaseByIdQuery, Sora_AppProject.Domain.Models.PrendasBase?>
{
    private readonly ApplicationDbContext _context;
    public GetPrendaBaseByIdQueryHandler(ApplicationDbContext context) => _context = context;

    public async Task<Sora_AppProject.Domain.Models.PrendasBase?> Handle(GetPrendaBaseByIdQuery request, CancellationToken cancellationToken)
        => await _context.PrendasBases.FindAsync(new object[] { request.Id }, cancellationToken);
}
