using MediatR;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Domain.Repositories;

namespace Sora_AppProject.Application.UseCases.SolicitudTallas.Commands;

public sealed record AddSolicitudTallaCommand(int SolicitudId, string Talla, int Cantidad) : IRequest<int>;

public sealed class AddSolicitudTallaCommandHandler
    : IRequestHandler<AddSolicitudTallaCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddSolicitudTallaCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(
        AddSolicitudTallaCommand request,
        CancellationToken cancellationToken)
    {
        var solicitudTalla = new SolicitudTalla
        {
            SolicitudId = request.SolicitudId,
            Talla = request.Talla,
            Cantidad = request.Cantidad
        };

        await _unitOfWork.SolicitudTallaRepository.AddAsync(solicitudTalla, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return solicitudTalla.Id;
    }
}
