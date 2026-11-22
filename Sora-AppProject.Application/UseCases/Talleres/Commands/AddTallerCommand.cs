using MediatR;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Domain.Ports.Repositories;

namespace Sora_AppProject.Application.UseCases.Talleres.Commands;

public class AddTallerCommand : IRequest<int>
{
    public int UsuarioId { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Direccion { get; set; }
    public string? Telefono { get; set; }
}

internal sealed class AddTallerCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<AddTallerCommand, int>
{
    public async Task<int> Handle(AddTallerCommand request, CancellationToken cancellationToken)
    {
        if (await unitOfWork.UsuarioRepository.GetByIdAsync(request.UsuarioId) is null)
            throw new KeyNotFoundException("Usuario no encontrado.");

        if (await unitOfWork.TallerRepository.ExistsByUsuarioIdAsync(request.UsuarioId))
            throw new InvalidOperationException("Este usuario ya tiene un taller registrado.");

        var taller = new Tallere
        {
            UsuarioId = request.UsuarioId,
            Nombre = request.Nombre,
            Direccion = request.Direccion,
            Telefono = request.Telefono
        };

        await unitOfWork.TallerRepository.AddAsync(taller);
        await unitOfWork.SaveChangesAsync();
        return taller.Id;
    }
}