using MediatR;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.PrendasBase.Commands;

public class UpdatePrendaBaseCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string Nombre { get; set; } = null!;
    public string Categoria { get; set; } = null!;
    public string? Descripcion { get; set; }
    public string? Modelo3dUrl { get; set; }
}

internal sealed class UpdatePrendaBaseCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UpdatePrendaBaseCommand, Unit>
{
    public async Task<Unit> Handle(UpdatePrendaBaseCommand request, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.PrendaBaseRepository.GetByIdAsync(request.Id)
                     ?? throw new KeyNotFoundException("Prenda base no encontrada.");

        entity.Nombre = request.Nombre;
        entity.Categoria = request.Categoria;
        entity.Descripcion = request.Descripcion;
        entity.Modelo3dUrl = request.Modelo3dUrl;

        await unitOfWork.PrendaBaseRepository.UpdateAsync(entity);
        await unitOfWork.SaveChangesAsync();
        return Unit.Value;
    }
}