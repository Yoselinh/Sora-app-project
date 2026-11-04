namespace Sora_AppProject.Domain.Ports.Repositories;

public interface IUnitOfWork
{
    ISolicitudCotizacionRepository SolicitudCotizacionRepository { get; }

    ISolicitudTallaRepository SolicitudTallaRepository { get; }

    IDisenoRepository DisenoRepository { get; }

    IDisenoTelaRepository DisenoTelaRepository { get; }

    Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default);
}