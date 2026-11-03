namespace Sora_AppProject.Domain.Repositories;

public interface IUnitOfWork
{
    ISolicitudCotizacionRepository SolicitudCotizacionRepository { get; }

    ISolicitudTallaRepository SolicitudTallaRepository { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
