using MediatR;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Domain.Repositories;

namespace Sora_AppProject.Application.UseCases.Disenos.Queries;

public record GetAllDisenosQuery()
    : IRequest<IReadOnlyList<Diseno>>;

internal sealed class GetAllDisenosQueryHandler
    : IRequestHandler<GetAllDisenosQuery, IReadOnlyList<Diseno>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllDisenosQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<Diseno>> Handle(
        GetAllDisenosQuery request,
        CancellationToken cancellationToken)
    {
        return await _unitOfWork.DisenoRepository
            .GetAllAsync(cancellationToken);
    }
}