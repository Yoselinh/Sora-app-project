using MediatR;
using Sora_AppProject.Application.Dtos;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.Disenos.Queries;

public class GetAllDisenosQuery : IRequest<List<DisenoDto>>
{
}

internal sealed class GetAllDisenosQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllDisenosQuery, List<DisenoDto>>
{
    public async Task<List<DisenoDto>> Handle(
        GetAllDisenosQuery request,
        CancellationToken cancellationToken)
    {
        var disenos = await unitOfWork.DisenoRepository.GetAllAsync();

        return disenos.Select(d => new DisenoDto
        {
            Id = d.Id,
            ClienteId = d.ClienteId,
            PrendaBaseId = d.PrendaBaseId,
            Nombre = d.Nombre,
            ConfiguracionVisor = d.ConfiguracionVisor,
            FechaCreacion = d.FechaCreacion
        }).ToList();
    }
}