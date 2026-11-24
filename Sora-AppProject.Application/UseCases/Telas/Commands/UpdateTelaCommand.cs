using MediatR;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.Telas.Commands;

public class UpdateTelaCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Composicion { get; set; }
}

internal sealed class UpdateTelaCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateTelaCommand, Unit>
{
    public async Task<Unit> Handle(UpdateTelaCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.TelaRepository.GetByIdAsync(request.Id)
                     ?? throw new KeyNotFoundException("Tela no encontrada.");

        entity.Nombre = request.Nombre;
        entity.Composicion = request.Composicion;

        await unitOfWork.TelaRepository.UpdateAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return Unit.Value;
    }
}