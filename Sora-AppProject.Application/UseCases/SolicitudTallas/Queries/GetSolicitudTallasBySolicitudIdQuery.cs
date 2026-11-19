using MediatR;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.SolicitudTallas.Queries;

public sealed record GetSolicitudTallasBySolicitudIdQuery(int SolicitudId)
    : IRequest<IReadOnlyList<SolicitudTalla>>;

public sealed class GetSolicitudTallasBySolicitudIdQueryHandler
    : IRequestHandler<GetSolicitudTallasBySolicitudIdQuery, IReadOnlyList<SolicitudTalla>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSolicitudTallasBySolicitudIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<IReadOnlyList<SolicitudTalla>> Handle(
        GetSolicitudTallasBySolicitudIdQuery request,
        CancellationToken cancellationToken) =>
        _unitOfWork.SolicitudTallaRepository.GetBySolicitudIdAsync(request.SolicitudId, cancellationToken);
}
