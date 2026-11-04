using MediatR;
using Sora_AppProject.Domain.Repositories;

namespace Sora_AppProject.Application.UseCases.DisenoTelas.Commands;

public record UpdateDisenoTelaCommand(
    int Id,
    int DisenoId,
    int TelaId,
    string Parte
) : IRequest<Unit>;

internal sealed class UpdateDisenoTelaCommandHandler
    : IRequestHandler<UpdateDisenoTelaCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDisenoTelaCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(
        UpdateDisenoTelaCommand request,
        CancellationToken cancellationToken)
    {
        var disenoTela = await _unitOfWork.DisenoTelaRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (disenoTela is null)
            throw new KeyNotFoundException("Diseño tela no encontrado.");

        disenoTela.DisenoId = request.DisenoId;
        disenoTela.TelaId = request.TelaId;
        disenoTela.Parte = request.Parte;

        await _unitOfWork.DisenoTelaRepository.UpdateAsync(
            disenoTela,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}