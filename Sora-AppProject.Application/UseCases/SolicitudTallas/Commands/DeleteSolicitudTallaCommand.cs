using MediatR;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.SolicitudTallas.Commands;

public class DeleteSolicitudTallaCommand : IRequest<Unit>
{
    public int Id { get; set; }
}

internal sealed class DeleteSolicitudTallaCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteSolicitudTallaCommand, Unit>
{
    public async Task<Unit> Handle(DeleteSolicitudTallaCommand request, CancellationToken cancellationToken)
    {
        var solicitudTalla = await unitOfWork.SolicitudTallaRepository.GetByIdAsync(request.Id)
                             ?? throw new KeyNotFoundException("Talla de solicitud no encontrada.");

        await unitOfWork.SolicitudTallaRepository.DeleteAsync(solicitudTalla);
        await unitOfWork.SaveChangesAsync();
        return Unit.Value;
    }
}
