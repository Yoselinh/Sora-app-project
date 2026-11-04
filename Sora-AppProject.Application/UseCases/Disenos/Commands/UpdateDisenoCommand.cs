using MediatR;

namespace Sora_AppProject.Application.UseCases.Disenos.Commands;

public record UpdateDisenoCommand(
    int Id,
    int ClienteId,
    int PrendaBaseId,
    string Nombre,
    string ConfiguracionVisor
) : IRequest<Unit>;

internal sealed class UpdateDisenoCommandHandler
    : IRequestHandler<UpdateDisenoCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDisenoCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(
        UpdateDisenoCommand request,
        CancellationToken cancellationToken)
    {
        var diseno = await _unitOfWork.DisenoRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (diseno is null)
            throw new KeyNotFoundException("Diseño no encontrado.");

        diseno.ClienteId = request.ClienteId;
        diseno.PrendaBaseId = request.PrendaBaseId;
        diseno.Nombre = request.Nombre;
        diseno.ConfiguracionVisor = request.ConfiguracionVisor;

        await _unitOfWork.DisenoRepository.UpdateAsync(
            diseno,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}