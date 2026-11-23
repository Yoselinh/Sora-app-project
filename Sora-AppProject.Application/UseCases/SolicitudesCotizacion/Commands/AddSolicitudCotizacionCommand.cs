using MediatR;
using Sora_AppProject.Domain.Ports.Repositories;
using SolicitudCotizacionEntity = Sora_AppProject.Domain.Models.SolicitudesCotizacion;

namespace Sora_AppProject.Application.UseCases.SolicitudesCotizacion.Commands;

public class AddSolicitudCotizacionCommand : IRequest<int>
{
    public int ClienteId { get; set; }
    public int DisenoId { get; set; }
}

internal sealed class AddSolicitudCotizacionCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<AddSolicitudCotizacionCommand, int>
{
    public async Task<int> Handle(AddSolicitudCotizacionCommand request, CancellationToken cancellationToken)
    {
        if (await unitOfWork.UsuarioRepository.GetByIdAsync(request.ClienteId) is null)
        {
            throw new KeyNotFoundException("Cliente no encontrado.");
        }

        if (await unitOfWork.DisenoRepository.GetByIdAsync(request.DisenoId) is null)
        {
            throw new KeyNotFoundException("Diseño no encontrado.");
        }

        var solicitud = new SolicitudCotizacionEntity
        {
            ClienteId = request.ClienteId,
            DisenoId = request.DisenoId,
            Estado = "pendiente"
        };

        await unitOfWork.SolicitudCotizacionRepository.AddAsync(solicitud);
        await unitOfWork.SaveChangesAsync();
        return solicitud.Id;
    }
}
