using Sora_AppProject.Domain.Ports.Repositories;
using Sora_AppProject.Infrastructure.Context;

namespace Sora_AppProject.Infrastructure.Adapters.Repositories.Implementation;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(
        ApplicationDbContext context,
        ISolicitudCotizacionRepository solicitudCotizacionRepository,
        ISolicitudTallaRepository solicitudTallaRepository,
        IDisenoRepository disenoRepository,
        IDisenoTelaRepository disenoTelaRepository)
    {
        _context = context;

        SolicitudCotizacionRepository = solicitudCotizacionRepository;
        SolicitudTallaRepository = solicitudTallaRepository;
        DisenoRepository = disenoRepository;
        DisenoTelaRepository = disenoTelaRepository;
    }

    public ISolicitudCotizacionRepository SolicitudCotizacionRepository { get; }

    public ISolicitudTallaRepository SolicitudTallaRepository { get; }

    public IDisenoRepository DisenoRepository { get; }

    public IDisenoTelaRepository DisenoTelaRepository { get; }

    public Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}