using MediatR;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.DisenoTelas.Commands;

public class UpdateDisenoTelaCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public int DisenoId { get; set; }
    public int TelaId { get; set; }
    public string Parte { get; set; } = null!;
}

internal sealed class UpdateDisenoTelaCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateDisenoTelaCommand, Unit>
{
    public async Task<Unit> Handle(
        UpdateDisenoTelaCommand request,
        CancellationToken cancellationToken)
    {
        var disenoTela = await unitOfWork.DisenoTelaRepository
            .GetByIdAsync(request.Id);

        if (disenoTela is null)
            throw new KeyNotFoundException("Diseño tela no encontrado.");

        disenoTela.DisenoId = request.DisenoId;
        disenoTela.TelaId = request.TelaId;
        disenoTela.Parte = request.Parte;

        await unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}