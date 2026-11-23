using MediatR;
using Sora_AppProject.Application.Dtos;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.Telas.Commands;

public record AddTelaCommand(
    TelaDto Request
) : IRequest<int>;

internal sealed class AddTelaCommandHandler : IRequestHandler<AddTelaCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddTelaCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(
        AddTelaCommand request,
        CancellationToken cancellationToken)
    {
        var tela = new Tela
        {
            Nombre = request.Request.Nombre,
            Composicion = request.Request.Composicion
        };

        await _unitOfWork.TelaRepository.AddAsync(
            tela,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return tela.Id;
    }
}
