using MediatR;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.DisenoTelas.Commands;

public record DeleteDisenoTelaCommand(int Id) : IRequest<Unit>;

internal sealed class DeleteDisenoTelaCommandHandler
    : IRequestHandler<DeleteDisenoTelaCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDisenoTelaCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(
        DeleteDisenoTelaCommand request,
        CancellationToken cancellationToken)
    {
        var disenoTela = await _unitOfWork.DisenoTelaRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (disenoTela is null)
            throw new KeyNotFoundException("Diseño tela no encontrado.");

        await _unitOfWork.DisenoTelaRepository.DeleteAsync(
            disenoTela,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}