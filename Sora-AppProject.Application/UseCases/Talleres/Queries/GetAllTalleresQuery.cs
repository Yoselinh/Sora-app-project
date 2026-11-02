using MediatR;
using Microsoft.EntityFrameworkCore;
using Sora_AppProject.Domain.Models;
 
namespace Sora_AppProject.Application.UseCases.Talleres.Queries;
 
public class GetAllTalleresQuery : IRequest<IEnumerable<Tallere>>
{
}
 
internal sealed class GetAllTalleresQueryHandler
    : IRequestHandler<GetAllTalleresQuery, IEnumerable<Tallere>>
{
    private readonly ApplicationDbContext _context;
 
    public GetAllTalleresQueryHandler(
        ApplicationDbContext context)
    {
        _context = context;
    }
 
    public async Task<IEnumerable<Tallere>> Handle(
        GetAllTalleresQuery request,
        CancellationToken cancellationToken)
    {
        // Sin Include: así no se cargan Usuario (tiene PasswordHash) ni otras relaciones.
        return await _context.Talleres
            .AsNoTracking()
            .OrderBy(t => t.Nombre)
            .ToListAsync(cancellationToken);
    }
}