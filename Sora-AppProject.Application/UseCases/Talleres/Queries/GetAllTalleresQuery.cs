using MediatR;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Domain.Repositories;

namespace Sora_AppProject.Application.UseCases.Talleres.Queries;

public class GetAllTalleresQuery : IRequest<IReadOnlyList<Tallere>>
{
}

internal sealed class GetAllTalleresQueryHandler
    : IRequestHandler<GetAllTalleresQuery, IReadOnlyList<Tallere>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllTalleresQueryHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<Tallere>> Handle(
        GetAllTalleresQuery request,
        CancellationToken cancellationToken)
    {
        return await _unitOfWork.TallerRepository
            .GetAllAsync(cancellationToken);
    }
}