using MediatR;
using Sora_AppProject.Domain.Repositories;

namespace Sora_AppProject.Application.UseCases.Disenos.Commands;

public record DeleteDisenoCommand(int Id) : IRequest<Unit>;

internal sealed class DeleteDisenoCommandHandler
    : IRequestHandler<DeleteDisenoCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDisenoCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(
        DeleteDisenoCommand request,
        CancellationToken cancellationToken)
    {
        var diseno = await _unitOfWork.DisenoRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (diseno is null)
            throw new KeyNotFoundException("Diseño no encontrado.");

        await _unitOfWork.DisenoRepository.DeleteAsync(
            diseno,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}