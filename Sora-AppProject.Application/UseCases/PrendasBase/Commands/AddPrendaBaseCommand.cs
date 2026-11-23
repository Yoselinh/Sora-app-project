using MediatR;
using Sora_AppProject.Application.Dtos;
using Sora_AppProject.Domain.Ports.Repositories;
using PrendaBaseEntity = Sora_AppProject.Domain.Models.PrendasBase;

namespace Sora_AppProject.Application.UseCases.PrendasBase.Commands;

public record AddPrendaBaseCommand(
    PrendaBaseDto Request
) : IRequest<int>;

internal sealed class AddPrendaBaseCommandHandler : IRequestHandler<AddPrendaBaseCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddPrendaBaseCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(
        AddPrendaBaseCommand request,
        CancellationToken cancellationToken)
    {
        var prendaBase = new PrendaBaseEntity
        {
            Nombre = request.Request.Nombre,
            Categoria = request.Request.Categoria,
            Descripcion = request.Request.Descripcion,
            Modelo3dUrl = request.Request.Modelo3dUrl
        };

        await _unitOfWork.PrendaBaseRepository.AddAsync(
            prendaBase,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return prendaBase.Id;
    }
}
