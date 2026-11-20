using MediatR;
using Microsoft.EntityFrameworkCore;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Infrastructure.Context;

namespace Sora_AppProject.Application.UseCases.Talleres.Queries;
 
public class GetTallerByIdQuery : IRequest<Tallere?>
{
    public int Id { get; set; }
}
 
internal sealed class GetTallerByIdQueryHandler
    : IRequestHandler<GetTallerByIdQuery, Tallere?>
{
    private readonly ApplicationDbContext _context;
 
    public GetTallerByIdQueryHandler(
        ApplicationDbContext context)
    {
        _context = context;
    }
 
    public async Task<Tallere?> Handle(
        GetTallerByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Talleres
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
    }
}