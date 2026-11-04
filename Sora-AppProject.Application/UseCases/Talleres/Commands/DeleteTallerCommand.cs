using MediatR;
using Sora_AppProject.Domain.Repositories;

namespace Sora_AppProject.Application.UseCases.Talleres.Commands;

public class DeleteTallerCommand : IRequest<Unit>
{
    public int Id { get; set; }
}

internal sealed class DeleteTallerCommandHandler
    : IRequestHandler<DeleteTallerCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTallerCommandHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(
        DeleteTallerCommand request,
        CancellationToken cancellationToken)
    {
        var taller = await _unitOfWork.TallerRepository
            .GetByIdAsync(request.Id, cancellationToken);

        if (taller is null)
            throw new KeyNotFoundException("Taller no encontrado.");

        // Las cotizaciones tienen DeleteBehavior.Restrict: no se puede borrar un taller que ya cotizó.
        var tieneCotizaciones = await _unitOfWork.TallerRepository
            .HasCotizacionesAsync(request.Id, cancellationToken);

        if (tieneCotizaciones)
            throw new InvalidOperationException("No se puede eliminar el taller porque tiene cotizaciones registradas.");

        await _unitOfWork.TallerRepository.DeleteAsync(taller, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}