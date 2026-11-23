using MediatR;
using Sora_AppProject.Application.Dtos;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.Disenos.Queries;

public class GetDisenoByIdQuery : IRequest<DisenoDto>
{
    public int Id { get; set; }
}

internal sealed class GetDisenoByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetDisenoByIdQuery, DisenoDto>
{
    public async Task<DisenoDto> Handle(
        GetDisenoByIdQuery request,
        CancellationToken cancellationToken)
    {
        var diseno = await unitOfWork.DisenoRepository.GetByIdAsync(request.Id);

        if (diseno is null)
            throw new KeyNotFoundException("Diseño no encontrado.");

        return new DisenoDto
        {
            Id = diseno.Id,
            ClienteId = diseno.ClienteId,
            PrendaBaseId = diseno.PrendaBaseId,
            Nombre = diseno.Nombre,
            ConfiguracionVisor = diseno.ConfiguracionVisor,
            FechaCreacion = diseno.FechaCreacion
        };
    }
}