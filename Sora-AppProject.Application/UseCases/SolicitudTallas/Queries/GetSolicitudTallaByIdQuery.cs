using MediatR;
using Sora_AppProject.Application.Dtos;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.SolicitudTallas.Queries;

public class GetSolicitudTallaByIdQuery : IRequest<SolicitudTallaDto>
{
    public int Id { get; set; }
}

internal sealed class GetSolicitudTallaByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetSolicitudTallaByIdQuery, SolicitudTallaDto>
{
    public async Task<SolicitudTallaDto> Handle(
        GetSolicitudTallaByIdQuery request,
        CancellationToken cancellationToken)
    {
        var talla = await unitOfWork.SolicitudTallaRepository.GetByIdAsync(request.Id)
                    ?? throw new KeyNotFoundException("Talla de solicitud no encontrada.");

        return new SolicitudTallaDto
        {
            Id = talla.Id,
            SolicitudId = talla.SolicitudId,
            Talla = talla.Talla,
            Cantidad = talla.Cantidad
        };
    }
}
