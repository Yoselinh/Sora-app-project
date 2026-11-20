using MediatR;
using Microsoft.EntityFrameworkCore;
using Sora_AppProject.Domain.Models;
using Sora_AppProject.Infrastructure.Context;

namespace Sora_AppProject.Application.UseCases.Talleres.Commands;
 
public class AddTallerCommand : IRequest<int>
{
    public int UsuarioId { get; set; }
    public string Nombre { get; set; } = null!;
    public string? Direccion { get; set; }
    public string? Telefono { get; set; }
}
 
internal sealed class AddTallerCommandHandler
    : IRequestHandler<AddTallerCommand, int>
{
    private readonly ApplicationDbContext _context;
 
    public AddTallerCommandHandler(
        ApplicationDbContext context)
    {
        _context = context;
    }
 
    public async Task<int> Handle(
        AddTallerCommand request,
        CancellationToken cancellationToken)
    {
        var usuarioExiste = await _context.Usuarios
            .AnyAsync(u => u.Id == request.UsuarioId, cancellationToken);
 
        if (!usuarioExiste)
            throw new KeyNotFoundException("Usuario no encontrado.");
 
        var usuarioTieneTaller = await _context.Talleres
            .AnyAsync(t => t.UsuarioId == request.UsuarioId, cancellationToken);
 
        if (usuarioTieneTaller)
            throw new InvalidOperationException("Este usuario ya tiene un taller registrado.");
 
        var taller = new Tallere
        {
            UsuarioId = request.UsuarioId,
            Nombre = request.Nombre,
            Direccion = request.Direccion,
            Telefono = request.Telefono
        };
 
        await _context.Talleres.AddAsync(
            taller,
            cancellationToken
        );
 
        await _context.SaveChangesAsync(
            cancellationToken
        );
 
        return taller.Id;
    }
}