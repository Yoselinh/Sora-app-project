using MediatR;
using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Application.UseCases.DisenoTelas.Commands;

public record AddDisenoTelaCommand(
    int DisenoId,
    int TelaId,
    string Parte
) : IRequest<int>;

internal sealed class AddDisenoTelaCommandHandler
    : IRequestHandler<AddDisenoTelaCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddDisenoTelaCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(
        AddDisenoTelaCommand request,
        CancellationToken cancellationToken)
    {
        var disenoTela = new DisenoTela
        {
            DisenoId = request.DisenoId,
            TelaId = request.TelaId,
            Parte = request.Parte
        };

        await _unitOfWork.DisenoTelaRepository.AddAsync(
            disenoTela,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return disenoTela.Id;
    }
}