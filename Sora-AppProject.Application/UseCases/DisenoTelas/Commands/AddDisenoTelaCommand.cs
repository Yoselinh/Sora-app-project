using MediatR;
using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Application.UseCases.DisenoTelas.Commands;

public record AddDisenoTelaCommand(
    int DisenoId,
    int TelaId,
    string Parte
) : IRequest<int>;

internal sealed class AddDisenoTelaCommandHandler
    : IRequestHandler<AddDisenoTelaCommand, int>
{
    private readonly ApplicationDbContext _context;

    public AddDisenoTelaCommandHandler(
        ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(
        AddDisenoTelaCommand request,
        CancellationToken cancellationToken)
    {
        var disenoTela = new DisenoTela
        {
            DisenoId = request.DisenoId,
            TelaId = request.TelaId,
            Parte = request.Parte
        };

        await _context.DisenoTelas.AddAsync(
            disenoTela,
            cancellationToken
        );

        await _context.SaveChangesAsync(
            cancellationToken
        );

        return disenoTela.Id;
    }
}