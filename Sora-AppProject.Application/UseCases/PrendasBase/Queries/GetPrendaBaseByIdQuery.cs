using MediatR;
using Sora_AppProject.Application.Dtos;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.PrendasBase.Queries;

public record GetPrendaBaseByIdQuery(
    int Id
) : IRequest<PrendaBaseDto>;

internal sealed class GetPrendaBaseByIdQueryHandler : IRequestHandler<GetPrendaBaseByIdQuery, PrendaBaseDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPrendaBaseByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PrendaBaseDto> Handle(
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

        return new PrendaBaseDto
        {
            Id = prendaBase.Id,
            Nombre = prendaBase.Nombre,
            Categoria = prendaBase.Categoria,
            Descripcion = prendaBase.Descripcion,
            Modelo3dUrl = prendaBase.Modelo3dUrl
        };
    }
}
