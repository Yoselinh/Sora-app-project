using MediatR;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.Cotizaciones.Commands;

public class DeleteCotizacionCommand(int id) : IRequest<Unit>
{
    public int Id { get; set; } = id;
}

internal sealed class DeleteCotizacionCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteCotizacionCommand, Unit>
{
    public async Task<Unit> Handle(DeleteCotizacionCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.CotizacionRepository.GetByIdAsync(request.Id)
                     ?? throw new KeyNotFoundException("Cotización no encontrada.");

        await unitOfWork.CotizacionRepository.DeleteAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return Unit.Value;
    }
}