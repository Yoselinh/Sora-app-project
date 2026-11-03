using MediatR;
using Sora_AppProject.Domain.Repositories;

namespace Sora_AppProject.Application.UseCases.SolicitudTallas.Commands;

public sealed record DeleteSolicitudTallaCommand(int Id) : IRequest<bool>;

public sealed class DeleteSolicitudTallaCommandHandler
    : IRequestHandler<DeleteSolicitudTallaCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSolicitudTallaCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(
        DeleteSolicitudTallaCommand request,
        CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.SolicitudTallaRepository;
        var solicitudTalla = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (solicitudTalla is null)
        {
            return false;
        }

        await repository.DeleteAsync(solicitudTalla, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
