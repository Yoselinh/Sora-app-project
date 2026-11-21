using MediatR;
using Sora_AppProject.Application.Dtos;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.Talleres.Queries;

public class GetTallerByIdQuery : IRequest<TallerDto>
{
    public int Id { get; set; }
}

internal sealed class GetTallerByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetTallerByIdQuery, TallerDto>
{
    public async Task<TallerDto> Handle(GetTallerByIdQuery request, CancellationToken cancellationToken)
    {
        var taller = await unitOfWork.TallerRepository.GetByIdAsync(request.Id)
                     ?? throw new KeyNotFoundException("Taller no encontrado.");

        return new TallerDto
        {
            Id = taller.Id,
            UsuarioId = taller.UsuarioId,
            Nombre = taller.Nombre,
            Direccion = taller.Direccion,
            Telefono = taller.Telefono
        };
    }
}