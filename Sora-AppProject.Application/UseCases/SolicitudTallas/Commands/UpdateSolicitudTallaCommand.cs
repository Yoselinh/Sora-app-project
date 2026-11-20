using MediatR;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.SolicitudTallas.Commands;

public sealed record UpdateSolicitudTallaCommand(int Id, int Cantidad) : IRequest<bool>;

public sealed class UpdateSolicitudTallaCommandHandler
    : IRequestHandler<UpdateSolicitudTallaCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSolicitudTallaCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(
        UpdateSolicitudTallaCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.SolicitudTallaRepository;
        var solicitudTalla = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (solicitudTalla is null)
        {
            return false;
        }

        solicitudTalla.Cantidad = request.Cantidad;
        await repository.UpdateAsync(solicitudTalla, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
