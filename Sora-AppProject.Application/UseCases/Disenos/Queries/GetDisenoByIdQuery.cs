using MediatR;
using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Application.UseCases.Disenos.Queries;

public record GetDisenoByIdQuery(int Id)
    : IRequest<Diseno?>;

internal sealed class GetDisenoByIdQueryHandler
    : IRequestHandler<GetDisenoByIdQuery, Diseno?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetDisenoByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Diseno?> Handle(
        GetDisenoByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _unitOfWork.DisenoRepository.GetByIdAsync(
            request.Id,
            cancellationToken);
    }
}