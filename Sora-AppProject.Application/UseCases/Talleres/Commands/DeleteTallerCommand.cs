using MediatR;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.Talleres.Commands;

public class DeleteTallerCommand : IRequest<Unit>
{
    public int Id { get; set; }
}

internal sealed class DeleteTallerCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteTallerCommand, Unit>
{
    public async Task<Unit> Handle(DeleteTallerCommand request, CancellationToken cancellationToken)
    {
        var taller = await unitOfWork.TallerRepository.GetByIdAsync(request.Id)
                     ?? throw new KeyNotFoundException("Taller no encontrado.");

        if (await unitOfWork.TallerRepository.HasCotizacionesAsync(request.Id))
            throw new InvalidOperationException("No se puede eliminar el taller porque tiene cotizaciones registradas.");

        await unitOfWork.TallerRepository.DeleteAsync(taller);
        await unitOfWork.SaveChangesAsync();
        return Unit.Value;
    }
}