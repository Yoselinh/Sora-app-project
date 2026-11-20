using MediatR;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.Cotizaciones.Commands;

public class DeleteCotizacionCommand : IRequest<Unit>
{
    public int Id { get; set; }

    public DeleteCotizacionCommand(int id)
    {
        Id = id;
    }
}

internal sealed class DeleteCotizacionCommandHandler : IRequestHandler<DeleteCotizacionCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCotizacionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteCotizacionCommand command, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.CotizacionRepository.GetByIdAsync(command.Id);

        if (entity is null)
        {
            throw new KeyNotFoundException("Cotización no encontrada.");
        }

        await _unitOfWork.CotizacionRepository.DeleteAsync(entity);
        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}
