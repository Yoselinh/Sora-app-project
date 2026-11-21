using MediatR;
using Sora_AppProject.Application.Dtos;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.TallerTelas.Queries;

public class GetTelasByTallerQuery : IRequest<List<TallerTelaDto>>
{
    public int TallerId { get; set; }
}

internal sealed class GetTelasByTallerQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetTelasByTallerQuery, List<TallerTelaDto>>
{
    public async Task<List<TallerTelaDto>> Handle(GetTelasByTallerQuery request, CancellationToken cancellationToken)
    {
        var tallerTelas = await unitOfWork.TallerTelaRepository.GetByTallerIdAsync(request.TallerId);

        return tallerTelas.Select(tt => new TallerTelaDto
        {
            TallerId = tt.TallerId,
            TelaId = tt.TelaId,
            PrecioPorMetro = tt.PrecioPorMetro
        }).ToList();
    }
}