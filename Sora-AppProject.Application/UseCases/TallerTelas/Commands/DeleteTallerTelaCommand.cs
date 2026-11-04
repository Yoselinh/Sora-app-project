using MediatR;
using Sora_AppProject.Domain.Repositories;

namespace Sora_AppProject.Application.UseCases.TallerTelas.Commands;

public class DeleteTallerTelaCommand : IRequest<Unit>
{
    public int TallerId { get; set; }
    public int TelaId { get; set; }
}

internal sealed class DeleteTallerTelaCommandHandler
    : IRequestHandler<DeleteTallerTelaCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTallerTelaCommandHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(
        DeleteTallerTelaCommand request,
        CancellationToken cancellationToken)
    {
        var tallerTela = await _unitOfWork.TallerTelaRepository
            .GetByIdAsync(request.TallerId, request.TelaId, cancellationToken);

        if (tallerTela is null)
            throw new KeyNotFoundException("El taller no ofrece esta tela.");

        await _unitOfWork.TallerTelaRepository.DeleteAsync(tallerTela, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}