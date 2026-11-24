using MediatR;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.PrendasBase.Commands;

public class DeletePrendaBaseCommand(int id) : IRequest<Unit>
{
    public int Id { get; set; } = id;
}

internal sealed class DeletePrendaBaseCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeletePrendaBaseCommand, Unit>
{
    public async Task<Unit> Handle(DeletePrendaBaseCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.PrendaBaseRepository.GetByIdAsync(request.Id)
                     ?? throw new KeyNotFoundException("Prenda base no encontrada.");

        await unitOfWork.PrendaBaseRepository.DeleteAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return Unit.Value;
    }
}