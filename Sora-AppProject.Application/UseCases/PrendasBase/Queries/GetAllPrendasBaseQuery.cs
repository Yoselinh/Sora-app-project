using MediatR;
using Sora_AppProject.Application.Dtos.Common;
using Sora_AppProject.Application.Dtos.PrendasBase;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.PrendasBase.Queries;

public record GetAllPrendasBaseQuery(
    int PageNumber = 1,
    int PageSize = 10
) : IRequest<PaginatedDto<PrendaBaseResponseDto>>;

internal sealed class GetAllPrendasBaseQueryHandler : IRequestHandler<GetAllPrendasBaseQuery, PaginatedDto<PrendaBaseResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllPrendasBaseQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginatedDto<PrendaBaseResponseDto>> Handle(
        GetAllPrendasBaseQuery request,
        CancellationToken cancellationToken)
    {
        var allItems = await _unitOfWork.PrendaBaseRepository.GetAllAsync(cancellationToken);

        var totalCount = allItems.Count;
        var pagedItems = allItems
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(p => new PrendaBaseResponseDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Categoria = p.Categoria,
                Descripcion = p.Descripcion,
                Modelo3dUrl = p.Modelo3dUrl
            })
            .ToList();

        return new PaginatedDto<PrendaBaseResponseDto>
        {
            Items = pagedItems,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }
}
