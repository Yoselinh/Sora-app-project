using MediatR;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.SolicitudTallas.Queries;

public class GetTotalCantidadBySolicitudQuery : IRequest<int>
{
    public int SolicitudId { get; set; }
}

internal sealed class GetTotalCantidadBySolicitudQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetTotalCantidadBySolicitudQuery, int>
{
    public async Task<int> Handle(
        GetTotalCantidadBySolicitudQuery request,
        CancellationToken cancellationToken)
    {
        return await unitOfWork.SolicitudTallaRepository.GetTotalCantidadBySolicitudIdAsync(request.SolicitudId);
    }
}
