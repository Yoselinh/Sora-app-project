using MediatR;
using Microsoft.EntityFrameworkCore;
using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Application.UseCases.DisenoTelas.Queries;

public record GetAllDisenoTelasQuery
    : IRequest<IEnumerable<DisenoTela>>;

internal sealed class GetAllDisenoTelasQueryHandler
    : IRequestHandler<GetAllDisenoTelasQuery, IEnumerable<DisenoTela>>
{
    private readonly ApplicationDbContext _context;

    public GetAllDisenoTelasQueryHandler(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DisenoTela>> Handle(
        GetAllDisenoTelasQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.DisenoTelas
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}