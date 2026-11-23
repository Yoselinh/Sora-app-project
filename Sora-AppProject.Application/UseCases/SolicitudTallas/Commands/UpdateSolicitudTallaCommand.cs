using MediatR;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.SolicitudTallas.Commands;

public class UpdateSolicitudTallaCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string Talla { get; set; } = null!;
    public int Cantidad { get; set; }
}

internal sealed class UpdateSolicitudTallaCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateSolicitudTallaCommand, Unit>
{
    public async Task<Unit> Handle(UpdateSolicitudTallaCommand request, CancellationToken cancellationToken)
    {
        var solicitudTalla = await unitOfWork.SolicitudTallaRepository.GetByIdAsync(request.Id)
                             ?? throw new KeyNotFoundException("Talla de solicitud no encontrada.");

        solicitudTalla.Talla = request.Talla;
        solicitudTalla.Cantidad = request.Cantidad;
        await unitOfWork.SaveChangesAsync();
        return Unit.Value;
    }
}
