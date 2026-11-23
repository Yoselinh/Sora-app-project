using MediatR;
using Sora_AppProject.Application.Dtos.Common;
using Sora_AppProject.Application.Dtos;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.PrendasBase.Queries;

public record GetAllPrendasBaseQuery(
    int PageNumber = 1,
    int PageSize = 10
) : IRequest<PaginatedDto<PrendaBaseDto>>;

internal sealed class GetAllPrendasBaseQueryHandler : IRequestHandler<GetAllPrendasBaseQuery, PaginatedDto<PrendaBaseDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllPrendasBaseQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginatedDto<PrendaBaseDto>> Handle(
        GetAllPrendasBaseQuery request,
        CancellationToken cancellationToken)
    {
        var allItems = await _unitOfWork.PrendaBaseRepository.GetAllAsync(cancellationToken);

        var totalCount = allItems.Count;
        var pagedItems = allItems
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(p => new PrendaBaseDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Categoria = p.Categoria,
                Descripcion = p.Descripcion,
                Modelo3dUrl = p.Modelo3dUrl
            })
            .ToList();

        return new PaginatedDto<PrendaBaseDto>
        {
            Items = pagedItems,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }
}
