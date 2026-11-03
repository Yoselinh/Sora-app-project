using MediatR;
using Sora_AppProject.Domain.Repositories;

namespace Sora_AppProject.Application.UseCases.SolicitudTallas.Queries;

public sealed record GetTotalCantidadBySolicitudQuery(int SolicitudId) : IRequest<int>;

public sealed class GetTotalCantidadBySolicitudQueryHandler
    : IRequestHandler<GetTotalCantidadBySolicitudQuery, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTotalCantidadBySolicitudQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<int> Handle(
        GetTotalCantidadBySolicitudQuery request,
        CancellationToken cancellationToken) =>
        _unitOfWork.SolicitudTallaRepository.GetTotalCantidadBySolicitudIdAsync(request.SolicitudId, cancellationToken);
}
