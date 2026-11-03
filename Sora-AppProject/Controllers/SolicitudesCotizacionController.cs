using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sora_AppProject.Application.UseCases.SolicitudesCotizacion.Commands;
using Sora_AppProject.Application.UseCases.SolicitudesCotizacion.Queries;
using Sora_AppProject.Domain.Models;

namespace Sora_AppProject.Controllers;

[ApiController]
[Route("api/solicitudes-cotizacion")]
public sealed class SolicitudesCotizacionController : ControllerBase
{
    private readonly ISender _sender;

    public SolicitudesCotizacionController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<ActionResult<int>> Add(
        [FromBody] AddSolicitudCotizacionCommand command,
        CancellationToken cancellationToken)
    {
        var id = await _sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, id);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<SolicitudesCotizacion>>> GetAll(
        CancellationToken cancellationToken) =>
        Ok(await _sender.Send(new GetAllSolicitudesCotizacionQuery(), cancellationToken));

    [HttpGet("{id:int}")]
    public async Task<ActionResult<SolicitudesCotizacion>> GetById(
        int id,
        CancellationToken cancellationToken)
    {
        var solicitud = await _sender.Send(new GetSolicitudCotizacionByIdQuery(id), cancellationToken);
        return solicitud is null ? NotFound() : Ok(solicitud);
    }

    [HttpGet("cliente/{clienteId:int}")]
    public async Task<ActionResult<IReadOnlyList<SolicitudesCotizacion>>> GetByCliente(
        int clienteId,
        CancellationToken cancellationToken) =>
        Ok(await _sender.Send(new GetSolicitudesCotizacionByUsuarioQuery(clienteId), cancellationToken));

    [HttpGet("estado/{estado}")]
    public async Task<ActionResult<IReadOnlyList<SolicitudesCotizacion>>> GetByEstado(
        string estado,
        CancellationToken cancellationToken) =>
        Ok(await _sender.Send(new GetSolicitudesCotizacionByEstadoQuery(estado), cancellationToken));

    [HttpPut("{id:int}/estado")]
    public async Task<IActionResult> UpdateEstado(
        int id,
        [FromBody] string estado,
        CancellationToken cancellationToken)
    {
        var updated = await _sender.Send(
            new UpdateSolicitudCotizacionCommand(id, estado),
            cancellationToken);
        return updated ? NoContent() : NotFound();
    }
}
