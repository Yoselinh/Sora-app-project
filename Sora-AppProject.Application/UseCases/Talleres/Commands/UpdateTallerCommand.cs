using MediatR;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.Talleres.Commands;

public class UpdateTallerCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Direccion { get; set; }
    public string? Telefono { get; set; }
}

internal sealed class UpdateTallerCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateTallerCommand, Unit>
{
    public async Task<Unit> Handle(UpdateTallerCommand request, CancellationToken cancellationToken)
    {
        var taller = await unitOfWork.TallerRepository.GetByIdAsync(request.Id)
                     ?? throw new KeyNotFoundException("Taller no encontrado.");

        taller.Nombre = request.Nombre;
        taller.Direccion = request.Direccion;
        taller.Telefono = request.Telefono;

        await unitOfWork.SaveChangesAsync();
        return Unit.Value;
    }
}