using MediatR;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Domain.Repositories;

namespace Sora_AppProject.Application.UseCases.Disenos.Commands;

public record AddDisenoCommand(
    int ClienteId,
    int PrendaBaseId,
    string Nombre,
    string ConfiguracionVisor
) : IRequest<int>;

internal sealed class AddDisenoCommandHandler
    : IRequestHandler<AddDisenoCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddDisenoCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(
        AddDisenoCommand request,
        CancellationToken cancellationToken)
    {
        var diseno = new Diseno
        {
            ClienteId = request.ClienteId,
            PrendaBaseId = request.PrendaBaseId,
            Nombre = request.Nombre,
            ConfiguracionVisor = request.ConfiguracionVisor
        };

        await _unitOfWork.DisenoRepository.AddAsync(
            diseno,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return diseno.Id;
    }
}