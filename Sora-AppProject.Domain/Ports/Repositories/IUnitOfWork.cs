namespace Sora_AppProject.Domain.Ports.Repositories;

public interface IUnitOfWork
{
    ISolicitudCotizacionRepository SolicitudCotizacionRepository { get; }
    ISolicitudTallaRepository SolicitudTallaRepository { get; }
    IDisenoRepository DisenoRepository { get; }
    IDisenoTelaRepository DisenoTelaRepository { get; }
    IUsuarioRepository UsuarioRepository { get; }
    IPrendaBaseRepository PrendaBaseRepository { get; }
    ITelaRepository TelaRepository { get; }
    ICotizacionRepository CotizacionRepository { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
