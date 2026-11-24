using MediatR;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.PrendasBase.Commands;

public class AddPrendaBaseCommand : IRequest<Unit>
{
    public string Nombre { get; set; } = null!;
    public string Categoria { get; set; } = null!;
    public string? Descripcion { get; set; }
    public string? Modelo3dUrl { get; set; }
}

internal sealed class AddPrendaBaseCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<AddPrendaBaseCommand, Unit>
{
    public async Task<Unit> Handle(AddPrendaBaseCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Nombre))
            throw new ArgumentException("El nombre de la prenda base es obligatorio.");

        if (string.IsNullOrWhiteSpace(request.Categoria))
            throw new ArgumentException("La categorÃ­a de la prenda base es obligatoria.");

        await unitOfWork.PrendaBaseRepository.AddAsync(new Domain.Models.PrendasBase
        {
            Nombre = request.Nombre,
            Categoria = request.Categoria,
            Descripcion = request.Descripcion,
            Modelo3dUrl = request.Modelo3dUrl
        });

        await unitOfWork.SaveChangesAsync();
        return Unit.Value;
    }
}