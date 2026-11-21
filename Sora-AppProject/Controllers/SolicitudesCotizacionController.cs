using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sora_AppProject.Application.UseCases.SolicitudesCotizacion.Commands;
using Sora_AppProject.Application.UseCases.SolicitudesCotizacion.Queries;

namespace Sora_AppProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SolicitudesCotizacionController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllSolicitudesCotizacionQuery());
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await mediator.Send(new GetSolicitudCotizacionByIdQuery { Id = id });
        return Ok(result);
    }

    [HttpGet("cliente/{clienteId:int}")]
    public async Task<IActionResult> GetByCliente(int clienteId)
    {
        var result = await mediator.Send(new GetSolicitudesCotizacionByUsuarioQuery { ClienteId = clienteId });
        return Ok(result);
    }

    [HttpGet("estado/{estado}")]
    public async Task<IActionResult> GetByEstado(string estado)
    {
        var result = await mediator.Send(new GetSolicitudesCotizacionByEstadoQuery { Estado = estado });
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddSolicitudCotizacionCommand command)
    {
        var id = await mediator.Send(command);
        return Ok(new { id });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateSolicitudCotizacionCommand command)
    {
        command.Id = id;
        await mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await mediator.Send(new DeleteSolicitudCotizacionCommand { Id = id });
        return NoContent();
    }
}
