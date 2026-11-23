using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sora_AppProject.Application.UseCases.Talleres.Commands;
using Sora_AppProject.Application.UseCases.Talleres.Queries;

namespace Sora_AppProject.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // cliente, taller y admin pueden consultar
public class TalleresController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllTalleresQuery());
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await mediator.Send(new GetTallerByIdQuery { Id = id });
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "taller,admin")]
    public async Task<IActionResult> Add(AddTallerCommand command)
    {
        // El taller solo registra SU taller: el UsuarioId sale del token.
        // El admin puede registrarlo para cualquier usuario.
        if (!User.IsInRole("admin"))
        {
            var usuarioId = UsuarioIdActual();
            if (usuarioId is null) return Forbid();
            command.UsuarioId = usuarioId.Value;
        }

        var id = await mediator.Send(command);
        return Ok(new { id });
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "taller,admin")]
    public async Task<IActionResult> Update(int id, UpdateTallerCommand command)
    {
        if (!await EsAdminODuenoAsync(id)) return Forbid();

        command.Id = id;
        await mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "taller,admin")]
    public async Task<IActionResult> Delete(int id)
    {
        if (!await EsAdminODuenoAsync(id)) return Forbid();

        await mediator.Send(new DeleteTallerCommand { Id = id });
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