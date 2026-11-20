using MediatR;
using Sora_AppProject.Application.Dtos.Telas;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.Telas.Queries;

public record GetTelaByIdQuery(
    int Id
) : IRequest<TelaResponseDto>;

internal sealed class GetTelaByIdQueryHandler : IRequestHandler<GetTelaByIdQuery, TelaResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTelaByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TelaResponseDto> Handle(
        GetTelaByIdQuery request,
        CancellationToken cancellationToken)
    {
        var tela = await _unitOfWork.TelaRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (tela is null)
        {
            throw new KeyNotFoundException("Tela no encontrada.");
        }

        return new TelaResponseDto
        {
            Id = tela.Id,
            Nombre = tela.Nombre,
            Composicion = tela.Composicion
        };
    }
}
