using MediatR;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Domain.Repositories;

namespace Sora_AppProject.Application.UseCases.Talleres.Queries;

public class GetTallerByIdQuery : IRequest<Tallere?>
{
    public int Id { get; set; }
}

internal sealed class GetTallerByIdQueryHandler
    : IRequestHandler<GetTallerByIdQuery, Tallere?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTallerByIdQueryHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Tallere?> Handle(
        GetTallerByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _unitOfWork.TallerRepository
            .GetByIdAsync(request.Id, cancellationToken);
    }
}