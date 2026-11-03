using MediatR;
using Microsoft.EntityFrameworkCore;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Infrastructure.Context;

namespace Sora_AppProject.Application.UseCases.DisenoTelas.Queries;

public record GetDisenoTelaByIdQuery(
    int Id
) : IRequest<DisenoTela?>;

internal sealed class GetDisenoTelaByIdQueryHandler
    : IRequestHandler<GetDisenoTelaByIdQuery, DisenoTela?>
{
    private readonly ApplicationDbContext _context;

    public GetDisenoTelaByIdQueryHandler(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DisenoTela?> Handle(
        GetDisenoTelaByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.DisenoTelas
            .AsNoTracking()
            .FirstOrDefaultAsync(
                dt => dt.Id == request.Id,
                cancellationToken
            );
    }
}