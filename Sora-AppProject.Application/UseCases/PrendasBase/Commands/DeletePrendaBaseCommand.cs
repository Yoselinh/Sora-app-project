using MediatR;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.PrendasBase.Commands;

public record DeletePrendaBaseCommand(
    int Id
) : IRequest<Unit>;

internal sealed class DeletePrendaBaseCommandHandler : IRequestHandler<DeletePrendaBaseCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeletePrendaBaseCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(
        DeletePrendaBaseCommand request,
        CancellationToken cancellationToken)
    {
        var prendaBase = await _unitOfWork.PrendaBaseRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (prendaBase is null)
        {
            throw new KeyNotFoundException("Prenda base no encontrada.");
        }

        await _unitOfWork.PrendaBaseRepository.DeleteAsync(
            prendaBase,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
