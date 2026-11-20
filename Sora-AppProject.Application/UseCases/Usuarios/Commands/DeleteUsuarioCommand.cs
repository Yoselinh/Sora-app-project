using MediatR;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.Usuarios.Commands;

public class DeleteUsuarioCommand : IRequest<Unit>
{
    public int Id { get; set; }
}

internal sealed class DeleteUsuarioCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteUsuarioCommand, Unit>
{
    public async Task<Unit> Handle(DeleteUsuarioCommand request, CancellationToken cancellationToken)
    {
        var usuario = await unitOfWork.UsuarioRepository.GetByIdAsync(request.Id)
                      ?? throw new KeyNotFoundException("Usuario no encontrado.");

        await unitOfWork.UsuarioRepository.DeleteAsync(usuario);
        await unitOfWork.SaveChangesAsync();
        return Unit.Value;
    }
}