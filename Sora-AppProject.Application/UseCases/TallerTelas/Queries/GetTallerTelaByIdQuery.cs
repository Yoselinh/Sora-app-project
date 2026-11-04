using MediatR;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Domain.Repositories;

namespace Sora_AppProject.Application.UseCases.TallerTelas.Queries;

public class GetTallerTelaByIdQuery : IRequest<TallerTela?>
{
    public int TallerId { get; set; }
    public int TelaId { get; set; }
}

internal sealed class GetTallerTelaByIdQueryHandler
    : IRequestHandler<GetTallerTelaByIdQuery, TallerTela?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTallerTelaByIdQueryHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TallerTela?> Handle(
        GetTallerTelaByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _unitOfWork.TallerTelaRepository
            .GetByIdAsync(request.TallerId, request.TelaId, cancellationToken);
    }
}