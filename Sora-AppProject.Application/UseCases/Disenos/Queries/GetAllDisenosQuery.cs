using MediatR;
using Microsoft.EntityFrameworkCore;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Infrastructure.Context;

namespace Sora_AppProject.Application.UseCases.Disenos.Queries;

public record GetAllDisenosQuery
    : IRequest<IEnumerable<Diseno>>;

internal sealed class GetAllDisenosQueryHandler
    : IRequestHandler<GetAllDisenosQuery, IEnumerable<Diseno>>
{
    private readonly ApplicationDbContext _context;

    public GetAllDisenosQueryHandler(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Diseno>> Handle(
        GetAllDisenosQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Disenos
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}