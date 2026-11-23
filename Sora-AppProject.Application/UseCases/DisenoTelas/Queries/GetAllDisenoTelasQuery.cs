using MediatR;
using Sora_AppProject.Application.Dtos;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.DisenoTelas.Queries;

public class GetAllDisenoTelasQuery : IRequest<List<DisenoTelaDto>>
{
}

internal sealed class GetAllDisenoTelasQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllDisenoTelasQuery, List<DisenoTelaDto>>
{
    public async Task<List<DisenoTelaDto>> Handle(
        GetAllDisenoTelasQuery request,
        CancellationToken cancellationToken)
    {
        var disenoTelas = await unitOfWork.DisenoTelaRepository.GetAllAsync();

        return disenoTelas.Select(dt => new DisenoTelaDto
        {
            Id = dt.Id,
            DisenoId = dt.DisenoId,
            TelaId = dt.TelaId,
            Parte = dt.Parte
        }).ToList();
    }
}