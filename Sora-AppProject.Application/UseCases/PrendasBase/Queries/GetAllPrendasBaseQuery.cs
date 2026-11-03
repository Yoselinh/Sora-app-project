using MediatR;
using Microsoft.EntityFrameworkCore;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Infrastructure.Context;

namespace Sora_AppProject.Application.UseCases.PrendasBase.Queries;

public record GetAllPrendasBaseQuery : IRequest<List<Sora_AppProject.Domain.Models.PrendasBase>>;

public class GetAllPrendasBaseQueryHandler : IRequestHandler<GetAllPrendasBaseQuery, List<Sora_AppProject.Domain.Models.PrendasBase>>
{
    private readonly ApplicationDbContext _context;
    public GetAllPrendasBaseQueryHandler(ApplicationDbContext context) => _context = context;

    public async Task<List<Sora_AppProject.Domain.Models.PrendasBase>> Handle(GetAllPrendasBaseQuery request, CancellationToken cancellationToken)
        => await _context.PrendasBases.ToListAsync(cancellationToken);
}
