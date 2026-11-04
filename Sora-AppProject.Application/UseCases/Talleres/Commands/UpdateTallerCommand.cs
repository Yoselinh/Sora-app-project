using MediatR;
using Sora_AppProject.Domain.Repositories;

namespace Sora_AppProject.Application.UseCases.Talleres.Commands;

public class UpdateTallerCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Direccion { get; set; }
    public string? Telefono { get; set; }
}

internal sealed class UpdateTallerCommandHandler
    : IRequestHandler<UpdateTallerCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTallerCommandHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(
        UpdateTallerCommand request,
        CancellationToken cancellationToken)
    {
        var taller = await _unitOfWork.TallerRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (taller is null)
            throw new KeyNotFoundException("Taller no encontrado.");

        taller.Nombre = request.Nombre;
        taller.Direccion = request.Direccion;
        taller.Telefono = request.Telefono;

        await _unitOfWork.TallerRepository.UpdateAsync(taller, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}