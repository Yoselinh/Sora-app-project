using MediatR;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.DisenoTelas.Commands;

public class DeleteDisenoTelaCommand : IRequest<Unit>
{
    public int Id { get; set; }
}

internal sealed class DeleteDisenoTelaCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteDisenoTelaCommand, Unit>
{
    public async Task<Unit> Handle(
        DeleteDisenoTelaCommand request,
        CancellationToken cancellationToken)
    {
        var disenoTela = await unitOfWork.DisenoTelaRepository
            .GetByIdAsync(request.Id);

        if (disenoTela is null)
            throw new KeyNotFoundException("Diseño tela no encontrado.");

        await unitOfWork.DisenoTelaRepository.DeleteAsync(disenoTela);
        await unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}