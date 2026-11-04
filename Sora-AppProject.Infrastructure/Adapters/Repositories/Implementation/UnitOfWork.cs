using Sora_AppProject.Domain.Repositories;
using Sora_AppProject.Infrastructure.Context;

namespace Sora_AppProject.Infrastructure.Repositories.Implementation;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(
        ApplicationDbContext context,
        ISolicitudCotizacionRepository solicitudCotizacionRepository,
        ISolicitudTallaRepository solicitudTallaRepository)
    {
        _context = context;
        SolicitudCotizacionRepository = solicitudCotizacionRepository;
        SolicitudTallaRepository = solicitudTallaRepository;
    }

    public ISolicitudCotizacionRepository SolicitudCotizacionRepository { get; }

    public ISolicitudTallaRepository SolicitudTallaRepository { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        _context.SaveChangesAsync(cancellationToken);
}
