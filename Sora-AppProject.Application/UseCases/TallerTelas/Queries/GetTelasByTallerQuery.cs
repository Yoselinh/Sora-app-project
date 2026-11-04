using MediatR;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Domain.Repositories;

namespace Sora_AppProject.Application.UseCases.TallerTelas.Queries;

public class GetTelasByTallerQuery : IRequest<IReadOnlyList<TallerTela>>
{
    public int TallerId { get; set; }
}

internal sealed class GetTelasByTallerQueryHandler
    : IRequestHandler<GetTelasByTallerQuery, IReadOnlyList<TallerTela>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTelasByTallerQueryHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<TallerTela>> Handle(
        GetTelasByTallerQuery request,
        CancellationToken cancellationToken)
    {
        return await _unitOfWork.TallerTelaRepository
            .GetByTallerIdAsync(request.TallerId, cancellationToken);
    }
}