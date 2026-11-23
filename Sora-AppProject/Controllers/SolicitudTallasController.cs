using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sora_AppProject.Application.UseCases.SolicitudTallas.Commands;
using Sora_AppProject.Application.UseCases.SolicitudTallas.Queries;

namespace Sora_AppProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SolicitudTallasController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllSolicitudTallasQuery());
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await mediator.Send(new GetSolicitudTallaByIdQuery { Id = id });
        return Ok(result);
    }

    [HttpGet("solicitud/{solicitudId:int}")]
    public async Task<IActionResult> GetBySolicitud(int solicitudId)
    {
        var result = await mediator.Send(new GetSolicitudTallasBySolicitudIdQuery { SolicitudId = solicitudId });
        return Ok(result);
    }

    [HttpGet("solicitud/{solicitudId:int}/total-cantidad")]
    public async Task<IActionResult> GetTotalCantidad(int solicitudId)
    {
        var result = await mediator.Send(new GetTotalCantidadBySolicitudQuery { SolicitudId = solicitudId });
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddSolicitudTallaCommand command)
    {
        var id = await mediator.Send(command);
        return Ok(new { id });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateSolicitudTallaCommand command)
    {
        command.Id = id;
        await mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await mediator.Send(new DeleteSolicitudTallaCommand { Id = id });
        return NoContent();
    }
}
