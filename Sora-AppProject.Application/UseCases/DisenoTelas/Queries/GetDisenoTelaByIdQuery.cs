using MediatR;
using Sora_AppProject.Application.Dtos;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.DisenoTelas.Queries;

public class GetDisenoTelaByIdQuery : IRequest<DisenoTelaDto>
{
    public int Id { get; set; }
}

internal sealed class GetDisenoTelaByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetDisenoTelaByIdQuery, DisenoTelaDto>
{
    public async Task<DisenoTelaDto> Handle(
        GetDisenoTelaByIdQuery request,
        CancellationToken cancellationToken)
    {
        var disenoTela = await unitOfWork.DisenoTelaRepository
            .GetByIdAsync(request.Id);

        if (disenoTela is null)
            throw new KeyNotFoundException("Diseño tela no encontrado.");

        return new DisenoTelaDto
        {
            Id = disenoTela.Id,
            DisenoId = disenoTela.DisenoId,
            TelaId = disenoTela.TelaId,
            Parte = disenoTela.Parte
        };
    }
}