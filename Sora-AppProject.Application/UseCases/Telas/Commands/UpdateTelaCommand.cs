using MediatR;
using Sora_AppProject.Application.Dtos.Telas;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.Telas.Commands;

public record UpdateTelaCommand(
    int Id,
    UpdateTelaRequestDto Request
) : IRequest<Unit>;

internal sealed class UpdateTelaCommandHandler : IRequestHandler<UpdateTelaCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTelaCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(
        UpdateTelaCommand request,
        CancellationToken cancellationToken)
    {
        var tela = await _unitOfWork.TelaRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (tela is null)
        {
            throw new KeyNotFoundException("Tela no encontrada.");
        }

        tela.Nombre = request.Request.Nombre;
        tela.Composicion = request.Request.Composicion;

        await _unitOfWork.TelaRepository.UpdateAsync(
            tela,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
