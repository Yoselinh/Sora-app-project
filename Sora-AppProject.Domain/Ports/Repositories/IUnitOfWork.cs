namespace Sora_AppProject.Domain.Ports.Repositories;

public interface IUnitOfWork
{
    ISolicitudCotizacionRepository SolicitudCotizacionRepository { get; }

    ISolicitudTallaRepository SolicitudTallaRepository { get; }

    IDisenoRepository DisenoRepository { get; }

    IDisenoTelaRepository DisenoTelaRepository { get; }
    
    ITallerRepository TallerRepository { get; }
    
    ITallerTelaRepository TallerTelaRepository { get; }
    
    IUsuarioRepository UsuarioRepository { get; }

    Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default);
}