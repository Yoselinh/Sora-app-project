using MediatR;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Domain.Repositories;

namespace Sora_AppProject.Application.UseCases.DisenoTelas.Queries;

public record GetAllDisenoTelasQuery()
    : IRequest<IReadOnlyList<DisenoTela>>;

internal sealed class GetAllDisenoTelasQueryHandler
    : IRequestHandler<GetAllDisenoTelasQuery, IReadOnlyList<DisenoTela>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllDisenoTelasQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<DisenoTela>> Handle(
        GetAllDisenoTelasQuery request,
        CancellationToken cancellationToken)
    {
        return await _unitOfWork.DisenoTelaRepository
            .GetAllAsync(cancellationToken);
    }
}