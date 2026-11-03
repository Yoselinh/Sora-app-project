using MediatR;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Domain.Repositories;

namespace Sora_AppProject.Application.UseCases.SolicitudTallas.Queries;

public sealed record GetAllSolicitudTallasQuery : IRequest<IReadOnlyList<SolicitudTalla>>;

public sealed class GetAllSolicitudTallasQueryHandler
    : IRequestHandler<GetAllSolicitudTallasQuery, IReadOnlyList<SolicitudTalla>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllSolicitudTallasQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<IReadOnlyList<SolicitudTalla>> Handle(
        GetAllSolicitudTallasQuery request,
        CancellationToken cancellationToken) =>
        _unitOfWork.SolicitudTallaRepository.GetAllAsync(cancellationToken);
}
