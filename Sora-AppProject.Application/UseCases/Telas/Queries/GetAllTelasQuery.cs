using MediatR;
using Sora_AppProject.Application.Dtos.Common;
using Sora_AppProject.Application.Dtos;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.Telas.Queries;

public record GetAllTelasQuery(
    int PageNumber = 1,
    int PageSize = 10
) : IRequest<PaginatedDto<TelaDto>>;

internal sealed class GetAllTelasQueryHandler : IRequestHandler<GetAllTelasQuery, PaginatedDto<TelaDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllTelasQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PaginatedDto<TelaDto>> Handle(
        GetAllTelasQuery request,
        CancellationToken cancellationToken)
    {
        var allItems = await _unitOfWork.TelaRepository.GetAllAsync(cancellationToken);

        var totalCount = allItems.Count;
        var pagedItems = allItems
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(t => new TelaDto
            {
                Id = t.Id,
                Nombre = t.Nombre,
                Composicion = t.Composicion
            })
            .ToList();

        return new PaginatedDto<TelaDto>
        {
            Items = pagedItems,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }
}
