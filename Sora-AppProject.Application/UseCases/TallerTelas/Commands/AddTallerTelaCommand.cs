using MediatR;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Domain.Repositories;

namespace Sora_AppProject.Application.UseCases.TallerTelas.Commands;

public class AddTallerTelaCommand : IRequest<Unit>
{
    public int TallerId { get; set; }
    public int TelaId { get; set; }
    public decimal PrecioPorMetro { get; set; }
}

internal sealed class AddTallerTelaCommandHandler
    : IRequestHandler<AddTallerTelaCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddTallerTelaCommandHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(
        AddTallerTelaCommand request,
        CancellationToken cancellationToken)
    {
        if (request.PrecioPorMetro <= 0)
            throw new ArgumentException("El precio por metro debe ser mayor que 0.");

        var repository = _unitOfWork.TallerTelaRepository;

        if (!await repository.TallerExistsAsync(request.TallerId, cancellationToken))
            throw new KeyNotFoundException("Taller no encontrado.");

        if (!await repository.TelaExistsAsync(request.TelaId, cancellationToken))
            throw new KeyNotFoundException("Tela no encontrada.");

        if (await repository.ExistsAsync(request.TallerId, request.TelaId, cancellationToken))
            throw new InvalidOperationException("El taller ya ofrece esta tela.");

        var tallerTela = new TallerTela
        {
            TallerId = request.TallerId,
            TelaId = request.TelaId,
            PrecioPorMetro = request.PrecioPorMetro
        };

        await repository.AddAsync(tallerTela, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}