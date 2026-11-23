using MediatR;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.SolicitudTallas.Commands;

public class AddSolicitudTallaCommand : IRequest<int>
{
    public int SolicitudId { get; set; }
    public string Talla { get; set; } = null!;
    public int Cantidad { get; set; }
}

internal sealed class AddSolicitudTallaCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<AddSolicitudTallaCommand, int>
{
    public async Task<int> Handle(AddSolicitudTallaCommand request, CancellationToken cancellationToken)
    {
        if (await unitOfWork.SolicitudCotizacionRepository.GetByIdAsync(request.SolicitudId) is null)
        {
            throw new KeyNotFoundException("Solicitud de cotización no encontrada.");
        }

        var solicitudTalla = new SolicitudTalla
        {
            SolicitudId = request.SolicitudId,
            Talla = request.Talla,
            Cantidad = request.Cantidad
        };

        await unitOfWork.SolicitudTallaRepository.AddAsync(solicitudTalla);
        await unitOfWork.SaveChangesAsync();
        return solicitudTalla.Id;
    }
}
