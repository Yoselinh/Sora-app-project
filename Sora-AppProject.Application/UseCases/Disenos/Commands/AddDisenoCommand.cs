using MediatR;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.Disenos.Commands;

public class AddDisenoCommand : IRequest<int>
{
    public int ClienteId { get; set; }
    public int PrendaBaseId { get; set; }
    public string Nombre { get; set; } = null!;
    public string ConfiguracionVisor { get; set; } = null!;
}

internal sealed class AddDisenoCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<AddDisenoCommand, int>
{
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

        await unitOfWork.DisenoRepository.AddAsync(diseno);
        await unitOfWork.SaveChangesAsync();

        return diseno.Id;
    }
}