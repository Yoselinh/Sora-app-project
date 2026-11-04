using MediatR;
using Sora_AppProject.Domain.Repositories;

namespace Sora_AppProject.Application.UseCases.TallerTelas.Commands;

public class UpdateTallerTelaCommand : IRequest<Unit>
{
    public int TallerId { get; set; }
    public int TelaId { get; set; }
    public decimal PrecioPorMetro { get; set; }
}

internal sealed class UpdateTallerTelaCommandHandler
    : IRequestHandler<UpdateTallerTelaCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTallerTelaCommandHandler(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(
        UpdateTallerTelaCommand request,
        CancellationToken cancellationToken)
    {
        if (request.PrecioPorMetro <= 0)
            throw new ArgumentException("El precio por metro debe ser mayor que 0.");

        var tallerTela = await _unitOfWork.TallerTelaRepository
            .GetByIdAsync(request.TallerId, request.TelaId, cancellationToken);

        if (tallerTela is null)
            throw new KeyNotFoundException("El taller no ofrece esta tela.");

        tallerTela.PrecioPorMetro = request.PrecioPorMetro;

        await _unitOfWork.TallerTelaRepository.UpdateAsync(tallerTela, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}