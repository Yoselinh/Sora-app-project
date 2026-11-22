using MediatR;
using Sora_AppProject.Application.Dtos.PrendasBase;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.PrendasBase.Commands;

public record UpdatePrendaBaseCommand(
    int Id,
    UpdatePrendaBaseRequestDto Request
) : IRequest<Unit>;

internal sealed class UpdatePrendaBaseCommandHandler : IRequestHandler<UpdatePrendaBaseCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePrendaBaseCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(
        UpdatePrendaBaseCommand request,
        CancellationToken cancellationToken)
    {
        var prendaBase = await _unitOfWork.PrendaBaseRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (prendaBase is null)
        {
            throw new KeyNotFoundException("Prenda base no encontrada.");
        }

        prendaBase.Nombre = request.Request.Nombre;
        prendaBase.Categoria = request.Request.Categoria;
        prendaBase.Descripcion = request.Request.Descripcion;
        prendaBase.Modelo3dUrl = request.Request.Modelo3dUrl;

        await _unitOfWork.PrendaBaseRepository.UpdateAsync(
            prendaBase,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
