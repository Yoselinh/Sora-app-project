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
        IDisenoTelaRepository disenoTelaRepository,
        ITallerRepository tallerRepository,
        ITallerTelaRepository tallerTelaRepository,
        IUsuarioRepository usuarioRepository,
        IPrendaBaseRepository prendaBaseRepository,
        ITelaRepository telaRepository,
        ICotizacionRepository cotizacionRepository)
    {
        _context = context;

        SolicitudCotizacionRepository = solicitudCotizacionRepository;
        SolicitudTallaRepository = solicitudTallaRepository;
        DisenoRepository = disenoRepository;
        DisenoTelaRepository = disenoTelaRepository;
        TallerRepository = tallerRepository;
        TallerTelaRepository = tallerTelaRepository;
        UsuarioRepository = usuarioRepository;
        PrendaBaseRepository = prendaBaseRepository;
        TelaRepository = telaRepository;
        CotizacionRepository = cotizacionRepository;
    }

    public ISolicitudCotizacionRepository SolicitudCotizacionRepository { get; }
    public ISolicitudTallaRepository SolicitudTallaRepository { get; }
    public IDisenoRepository DisenoRepository { get; }
    public IDisenoTelaRepository DisenoTelaRepository { get; }
    public ITallerRepository TallerRepository { get; }
    public ITallerTelaRepository TallerTelaRepository { get; }
    public IUsuarioRepository UsuarioRepository { get; }
    public IPrendaBaseRepository PrendaBaseRepository { get; }
    public ITelaRepository TelaRepository { get; }
    public ICotizacionRepository CotizacionRepository { get; }

    public Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}
