using MediatR;
using Microsoft.EntityFrameworkCore;
using Sora_AppProject.Application.DTOs.Disenos;
using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Application.UseCases.Disenos.Queries;

public record GetAllDisenosQuery
    : IRequest<IEnumerable<DisenoDto>>;

internal sealed class GetAllDisenosQueryHandler
    : IRequestHandler<GetAllDisenosQuery, IEnumerable<DisenoDto>>
{
    private readonly ApplicationDbContext _context;

    public GetAllDisenosQueryHandler(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DisenoDto>> Handle(
        GetAllDisenosQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Disenos
            .AsNoTracking()
            .Select(d => new DisenoDto(
                d.Id,
                d.ClienteId,
                d.PrendaBaseId,
                d.Nombre,
                d.ConfiguracionVisor,
                d.FechaCreacion
            ))
            .ToListAsync(cancellationToken);
    }
}