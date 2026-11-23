using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sora_AppProject.Application.UseCases.Talleres.Queries;
using Sora_AppProject.Application.UseCases.TallerTelas.Commands;
using Sora_AppProject.Application.UseCases.TallerTelas.Queries;

namespace Sora_AppProject.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // el cliente puede ver las telas y precios de un taller
public class TallerTelasController(IMediator mediator) : ControllerBase
{
    [HttpGet("taller/{tallerId:int}")]
    public async Task<IActionResult> GetByTaller(int tallerId)
    {
        var result = await mediator.Send(new GetTelasByTallerQuery { TallerId = tallerId });
        return Ok(result);
    }

    [HttpGet("{tallerId:int}/{telaId:int}")]
    public async Task<IActionResult> GetById(int tallerId, int telaId)
    {
        var result = await mediator.Send(new GetTallerTelaByIdQuery { TallerId = tallerId, TelaId = telaId });
        return Ok(result);
    }

    //commands: solo el dueño del taller o el admin


    [HttpPost]
    [Authorize(Roles = "taller,admin")]
    public async Task<IActionResult> Add(AddTallerTelaCommand command)
    {
        if (!await EsAdminODuenoAsync(command.TallerId)) return Forbid();

        await mediator.Send(command);
        return Ok();
    }

    [HttpPut("{tallerId:int}/{telaId:int}")]
    [Authorize(Roles = "taller,admin")]
    public async Task<IActionResult> Update(int tallerId, int telaId, UpdateTallerTelaCommand command)
    {
        if (!await EsAdminODuenoAsync(tallerId)) return Forbid();

        command.TallerId = tallerId;
        command.TelaId = telaId;
        await mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{tallerId:int}/{telaId:int}")]
    [Authorize(Roles = "taller,admin")]
    public async Task<IActionResult> Delete(int tallerId, int telaId)
    {
        if (!await EsAdminODuenoAsync(tallerId)) return Forbid();

        await mediator.Send(new DeleteTallerTelaCommand { TallerId = tallerId, TelaId = telaId });
        return NoContent();
    }


    // ---------- helpers ----------

    private int? UsuarioIdActual()
    {
        var raw = User.FindFirstValue(ClaimTypes.NameIdentifier)
                  ?? User.FindFirstValue(JwtRegisteredClaimNames.Sub);
        return int.TryParse(raw, out var id) ? id : null;
    }

    private async Task<bool> EsAdminODuenoAsync(int tallerId)
    {
        if (User.IsInRole("admin")) return true;

        var usuarioId = UsuarioIdActual();
        if (usuarioId is null) return false;

        var taller = await mediator.Send(new GetTallerByIdQuery { Id = tallerId });
        return taller.UsuarioId == usuarioId;
    }
}