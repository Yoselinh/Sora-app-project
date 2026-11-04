    using MediatR;
    using Sora_AppProject.Domain.Models;
    using Sora_AppProject.Domain.Repositories;

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
        private readonly IUnitOfWork _unitOfWork;

        public AddTallerCommandHandler(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
     
        public async Task<int> Handle(
            AddTallerCommand request,
            CancellationToken cancellationToken)
        {
            var usuarioExiste = await _unitOfWork.TallerRepository
                .UsuarioExistsAsync(request.UsuarioId, cancellationToken);
     
            if (!usuarioExiste)
                throw new KeyNotFoundException("Usuario no encontrado.");
     
            var usuarioTieneTaller = await _unitOfWork.TallerRepository
                .ExistsByUsuarioIdAsync(request.UsuarioId, cancellationToken);
     
            if (usuarioTieneTaller)
                throw new InvalidOperationException("Este usuario ya tiene un taller registrado.");
     
            var taller = new Tallere
            {
                UsuarioId = request.UsuarioId,
                Nombre = request.Nombre,
                Direccion = request.Direccion,
                Telefono = request.Telefono
            };
     
            await _unitOfWork.TallerRepository.AddAsync(taller, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
     
            return taller.Id;
        }
    }