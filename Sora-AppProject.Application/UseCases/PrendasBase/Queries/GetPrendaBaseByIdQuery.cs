using MediatR;
using Sora_AppProject.Application.Dtos.PrendasBase;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.PrendasBase.Queries;

public record GetPrendaBaseByIdQuery(
    int Id
) : IRequest<PrendaBaseResponseDto>;

internal sealed class GetPrendaBaseByIdQueryHandler : IRequestHandler<GetPrendaBaseByIdQuery, PrendaBaseResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPrendaBaseByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PrendaBaseResponseDto> Handle(
        GetPrendaBaseByIdQuery request,
        CancellationToken cancellationToken)
    {
        var prendaBase = await _unitOfWork.PrendaBaseRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (prendaBase is null)
        {
            throw new KeyNotFoundException("Prenda base no encontrada.");
        }

        return new PrendaBaseResponseDto
        {
            Id = prendaBase.Id,
            Nombre = prendaBase.Nombre,
            Categoria = prendaBase.Categoria,
            Descripcion = prendaBase.Descripcion,
            Modelo3dUrl = prendaBase.Modelo3dUrl
        };
    }
}
