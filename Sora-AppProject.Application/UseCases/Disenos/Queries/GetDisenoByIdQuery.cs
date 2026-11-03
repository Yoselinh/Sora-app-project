using MediatR;
using Microsoft.EntityFrameworkCore;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Infrastructure.Context;

namespace Sora_AppProject.Application.UseCases.Disenos.Queries;

public record GetDisenoByIdQuery(
    int Id
) : IRequest<Diseno?>;

internal sealed class GetDisenoByIdQueryHandler
    : IRequestHandler<GetDisenoByIdQuery, Diseno?>
{
    private readonly ApplicationDbContext _context;

    public GetDisenoByIdQueryHandler(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Diseno?> Handle(
        GetDisenoByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Disenos
            .AsNoTracking()
            .FirstOrDefaultAsync(
                d => d.Id == request.Id,
                cancellationToken
            );
    }
}