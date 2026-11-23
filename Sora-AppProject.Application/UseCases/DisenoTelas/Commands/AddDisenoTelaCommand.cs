using MediatR;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.DisenoTelas.Commands;

public class AddDisenoTelaCommand : IRequest<int>
{
    public int DisenoId { get; set; }
    public int TelaId { get; set; }
    public string Parte { get; set; } = null!;
}

internal sealed class AddDisenoTelaCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<AddDisenoTelaCommand, int>
{
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

        await unitOfWork.DisenoTelaRepository.AddAsync(disenoTela);
        await unitOfWork.SaveChangesAsync();

        return disenoTela.Id;
    }
}