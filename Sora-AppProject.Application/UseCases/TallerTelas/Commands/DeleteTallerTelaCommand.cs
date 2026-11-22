using MediatR;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.TallerTelas.Commands;

public class DeleteTallerTelaCommand : IRequest<Unit>
{
    public int TallerId { get; set; }
    public int TelaId { get; set; }
}

internal sealed class DeleteTallerTelaCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteTallerTelaCommand, Unit>
{
    public async Task<Unit> Handle(DeleteTallerTelaCommand request, CancellationToken cancellationToken)
    {
        var tallerTela = await unitOfWork.TallerTelaRepository.GetByIdAsync(request.TallerId, request.TelaId)
                         ?? throw new KeyNotFoundException("El taller no ofrece esta tela.");

        await unitOfWork.TallerTelaRepository.DeleteAsync(tallerTela);
        await unitOfWork.SaveChangesAsync();
        return Unit.Value;
    }
}