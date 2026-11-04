using MediatR;
using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Application.UseCases.DisenoTelas.Queries;

public record GetDisenoTelaByIdQuery(int Id)
    : IRequest<DisenoTela?>;

internal sealed class GetDisenoTelaByIdQueryHandler
    : IRequestHandler<GetDisenoTelaByIdQuery, DisenoTela?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetDisenoTelaByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<DisenoTela?> Handle(
        GetDisenoTelaByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _unitOfWork.DisenoTelaRepository.GetByIdAsync(
            request.Id,
            cancellationToken);
    }
}