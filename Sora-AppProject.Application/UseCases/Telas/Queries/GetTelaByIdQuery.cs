using MediatR;
using Sora_AppProject.Application.Dtos;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.Telas.Queries;

public record GetTelaByIdQuery(
    int Id
) : IRequest<TelaDto>;

internal sealed class GetTelaByIdQueryHandler : IRequestHandler<GetTelaByIdQuery, TelaDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTelaByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TelaDto> Handle(
        GetTelaByIdQuery request, CancellationToken cancellationToken)
    {
        var tela = await _unitOfWork.TelaRepository.GetByIdAsync(
            request.Id);

        if (tela is null)
        {
            throw new KeyNotFoundException("Tela no encontrada.");
        }

        return new TelaDto
        {
            Id = tela.Id,
            Nombre = tela.Nombre,
            Composicion = tela.Composicion
        };
    }
}
