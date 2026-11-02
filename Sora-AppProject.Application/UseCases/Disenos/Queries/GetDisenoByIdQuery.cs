using MediatR;
using Microsoft.EntityFrameworkCore;
using Sora_AppProject.Application.DTOs.Disenos;
using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Application.UseCases.Disenos.Queries;

public record GetDisenoByIdQuery(
    int Id
) : IRequest<DisenoDto?>;

internal sealed class GetDisenoByIdQueryHandler
    : IRequestHandler<GetDisenoByIdQuery, DisenoDto?>
{
    private readonly ApplicationDbContext _context;

    public GetDisenoByIdQueryHandler(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DisenoDto?> Handle(
        GetDisenoByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Disenos
            .AsNoTracking()
            .Where(d => d.Id == request.Id)
            .Select(d => new DisenoDto(
                d.Id,
                d.ClienteId,
                d.PrendaBaseId,
                d.Nombre,
                d.ConfiguracionVisor,
                d.FechaCreacion
            ))
            .FirstOrDefaultAsync(cancellationToken);
    }
}