using MediatR;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.Disenos.Commands;

public class DeleteDisenoCommand : IRequest<Unit>
{
    public int Id { get; set; }
}

internal sealed class DeleteDisenoCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteDisenoCommand, Unit>
{
    public async Task<Unit> Handle(
        DeleteDisenoCommand request,
        CancellationToken cancellationToken)
    {
        var diseno = await unitOfWork.DisenoRepository.GetByIdAsync(request.Id);

        if (diseno is null)
            throw new KeyNotFoundException("Diseño no encontrado.");

        await unitOfWork.DisenoRepository.DeleteAsync(diseno);
        await unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}