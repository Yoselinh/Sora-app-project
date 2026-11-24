using MediatR;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.Telas.Commands;

public class DeleteTelaCommand(int id) : IRequest<Unit>
{
    public int Id { get; set; } = id;
}

internal sealed class DeleteTelaCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteTelaCommand, Unit>
{
    public async Task<Unit> Handle(DeleteTelaCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.TelaRepository.GetByIdAsync(request.Id)
                     ?? throw new KeyNotFoundException("Tela no encontrada.");

        await unitOfWork.TelaRepository.DeleteAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return Unit.Value;
    }
}