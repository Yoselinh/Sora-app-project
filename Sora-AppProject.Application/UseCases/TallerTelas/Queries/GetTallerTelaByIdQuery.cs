using MediatR;
using Sora_AppProject.Application.Dtos;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.TallerTelas.Queries;

public class GetTallerTelaByIdQuery : IRequest<TallerTelaDto>
{
    public int TallerId { get; set; }
    public int TelaId { get; set; }
}

internal sealed class GetTallerTelaByIdQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetTallerTelaByIdQuery, TallerTelaDto>
{
    public async Task<TallerTelaDto> Handle(GetTallerTelaByIdQuery request, CancellationToken cancellationToken)
    {
        var tallerTela = await unitOfWork.TallerTelaRepository.GetByIdAsync(request.TallerId, request.TelaId)
                         ?? throw new KeyNotFoundException("El taller no ofrece esta tela.");

        return new TallerTelaDto
        {
            TallerId = tallerTela.TallerId,
            TelaId = tallerTela.TelaId,
            PrecioPorMetro = tallerTela.PrecioPorMetro
        };
    }
}