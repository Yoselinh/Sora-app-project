using MediatR;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.Telas.Commands;

public class AddTelaCommand : IRequest<Unit>
{
    public string Nombre { get; set; } = null!;
    public string? Composicion { get; set; }
}

internal sealed class AddTelaCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<AddTelaCommand, Unit>
{
    public async Task<Unit> Handle(AddTelaCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Nombre))
            throw new ArgumentException("El nombre de la tela es obligatorio.");

        await unitOfWork.TelaRepository.AddAsync(new Tela
        {
            Nombre = request.Nombre,
            Composicion = request.Composicion
        });

        await unitOfWork.SaveChangesAsync();
        return Unit.Value;
    }
}