using MediatR;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.SolicitudTallas.Queries;

public sealed record GetSolicitudTallaByIdQuery(int Id) : IRequest<SolicitudTalla?>;

public sealed class GetSolicitudTallaByIdQueryHandler
    : IRequestHandler<GetSolicitudTallaByIdQuery, SolicitudTalla?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSolicitudTallaByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<SolicitudTalla?> Handle(
        GetSolicitudTallaByIdQuery request,
        CancellationToken cancellationToken) =>
        _unitOfWork.SolicitudTallaRepository.GetByIdAsync(request.Id, cancellationToken);
}
