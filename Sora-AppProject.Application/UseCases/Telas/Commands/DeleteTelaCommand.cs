using MediatR;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.Telas.Commands;

public record DeleteTelaCommand(
    int Id
) : IRequest<Unit>;

internal sealed class DeleteTelaCommandHandler : IRequestHandler<DeleteTelaCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTelaCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(
        DeleteTelaCommand request,
        CancellationToken cancellationToken)
    {
        var tela = await _unitOfWork.TelaRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (tela is null)
        {
            throw new KeyNotFoundException("Tela no encontrada.");
        }

        await _unitOfWork.TelaRepository.DeleteAsync(
            tela,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
