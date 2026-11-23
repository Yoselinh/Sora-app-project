using MediatR;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.Disenos.Commands;

public class UpdateDisenoCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public int PrendaBaseId { get; set; }
    public string Nombre { get; set; } = null!;
    public string ConfiguracionVisor { get; set; } = null!;
}

internal sealed class UpdateDisenoCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateDisenoCommand, Unit>
{
    public async Task<Unit> Handle(
        UpdateDisenoCommand request,
        CancellationToken cancellationToken)
    {
        var diseno = await unitOfWork.DisenoRepository.GetByIdAsync(request.Id);

        if (diseno is null)
            throw new KeyNotFoundException("Diseño no encontrado.");

        diseno.ClienteId = request.ClienteId;
        diseno.PrendaBaseId = request.PrendaBaseId;
        diseno.Nombre = request.Nombre;
        diseno.ConfiguracionVisor = request.ConfiguracionVisor;

        await unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}