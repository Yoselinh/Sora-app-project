using MediatR;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.TallerTelas.Commands;

public class AddTallerTelaCommand : IRequest<Unit>
{
    public int TallerId { get; set; }
    public int TelaId { get; set; }
    public decimal PrecioPorMetro { get; set; }
}

internal sealed class AddTallerTelaCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddTallerTelaCommand, Unit>
{
    public async Task<Unit> Handle(AddTallerTelaCommand request, CancellationToken cancellationToken)
    {
        if (request.PrecioPorMetro <= 0)
            throw new ArgumentException("El precio por metro debe ser mayor que 0.");

        if (await unitOfWork.TallerRepository.GetByIdAsync(request.TallerId) is null)
            throw new KeyNotFoundException("Taller no encontrado.");

        if (!await unitOfWork.TallerTelaRepository.TelaExistsAsync(request.TelaId))
            throw new KeyNotFoundException("Tela no encontrada.");

        if (await unitOfWork.TallerTelaRepository.ExistsAsync(request.TallerId, request.TelaId))
            throw new InvalidOperationException("El taller ya ofrece esta tela.");

        await unitOfWork.TallerTelaRepository.AddAsync(new TallerTela
        {
            TallerId = request.TallerId,
            TelaId = request.TelaId,
            PrecioPorMetro = request.PrecioPorMetro
        });

        await unitOfWork.SaveChangesAsync();
        return Unit.Value;
    }
}