using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sora_AppProject.Application.UseCases.SolicitudTallas.Commands;
using Sora_AppProject.Application.UseCases.SolicitudTallas.Queries;
using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Controllers;

[ApiController]
[Route("api/solicitud-tallas")]
public sealed class SolicitudTallasController : ControllerBase
{
    private readonly ISender _sender;

    public SolicitudTallasController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<ActionResult<int>> Add(
        [FromBody] AddSolicitudTallaCommand command,
        CancellationToken cancellationToken)
    {
        var id = await _sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<SolicitudTalla>>> GetAll(
        CancellationToken cancellationToken) =>
        Ok(await _sender.Send(new GetAllSolicitudTallasQuery(), cancellationToken));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<SolicitudTalla>> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var solicitudTalla = await _sender.Send(new GetSolicitudTallaByIdQuery(id), cancellationToken);
        return solicitudTalla is null ? NotFound() : Ok(solicitudTalla);
    }

    [HttpGet("solicitud/{solicitudId:int}")]
    public async Task<ActionResult<IReadOnlyList<SolicitudTalla>>> GetBySolicitud(
        int solicitudId,
        CancellationToken cancellationToken) =>
        Ok(await _sender.Send(new GetSolicitudTallasBySolicitudIdQuery(solicitudId), cancellationToken));

    [HttpGet("solicitud/{solicitudId:int}/total-cantidad")]
    public async Task<ActionResult<int>> GetTotalCantidad(
        int solicitudId,
        CancellationToken cancellationToken) =>
        Ok(await _sender.Send(new GetTotalCantidadBySolicitudQuery(solicitudId), cancellationToken));

    [HttpPut("{id:int}/cantidad")]
    public async Task<IActionResult> UpdateCantidad(
        int id,
        [FromBody] int cantidad,
        CancellationToken cancellationToken)
    {
        var updated = await _sender.Send(
            new UpdateSolicitudTallaCommand(id, cantidad),
            cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var deleted = await _sender.Send(new DeleteSolicitudTallaCommand(id), cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
