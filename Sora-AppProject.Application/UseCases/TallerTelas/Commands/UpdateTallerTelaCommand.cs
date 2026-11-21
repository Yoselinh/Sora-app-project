using MediatR;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.TallerTelas.Commands;

public class UpdateTallerTelaCommand : IRequest<Unit>
{
    public int TallerId { get; set; }
    public int TelaId { get; set; }
    public decimal PrecioPorMetro { get; set; }
}

internal sealed class UpdateTallerTelaCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateTallerTelaCommand, Unit>
{
    public async Task<Unit> Handle(UpdateTallerTelaCommand request, CancellationToken cancellationToken)
    {
        if (request.PrecioPorMetro <= 0)
            throw new ArgumentException("El precio por metro debe ser mayor que 0.");

        var tallerTela = await unitOfWork.TallerTelaRepository.GetByIdAsync(request.TallerId, request.TelaId)
                         ?? throw new KeyNotFoundException("El taller no ofrece esta tela.");

        tallerTela.PrecioPorMetro = request.PrecioPorMetro;

        await unitOfWork.SaveChangesAsync();
        return Unit.Value;
    }
}