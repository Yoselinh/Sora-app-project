using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sora_AppProject.Application.UseCases.Cotizaciones.Commands;
using Sora_AppProject.Application.UseCases.Cotizaciones.Queries;

namespace Sora_AppProject.Controllers;

[ApiController]
[Route("api/cotizaciones")]
[Authorize]
public class CotizacionesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var result = await mediator.Send(new GetAllCotizacionesQuery(pageNumber, pageSize));
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await mediator.Send(new GetCotizacionByIdQuery(id));
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "taller,admin")]
    public async Task<IActionResult> Add([FromBody] AddCotizacionCommand command)
    {
        await mediator.Send(command);
        return Ok(new { message = "Cotización creada correctamente" });
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "taller,admin")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCotizacionCommand command)
    {
        command.Id = id;
        await mediator.Send(command);
        return Ok(new { message = "Cotización actualizada correctamente" });
    }
    
    [HttpPut("{id:int}/accept")]
    [Authorize(Roles = "cliente")]
    public async Task<IActionResult> Accept(int id)
    {
        var clienteId = int.Parse(User.FindFirst("id")!.Value);

        await mediator.Send(new AcceptCotizacionCommand(id, clienteId));

        return Ok(new { message = "Cotización aceptada correctamente" });
    }

    [HttpPut("{id:int}/reject")]
    [Authorize(Roles = "cliente")]
    public async Task<IActionResult> Reject(int id)
    {
        var clienteId = int.Parse(User.FindFirst("id")!.Value);

        await mediator.Send(new RejectCotizacionCommand(id, clienteId));

        return Ok(new { message = "Cotización rechazada correctamente" });
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await mediator.Send(new DeleteCotizacionCommand(id));
        return Ok(new { message = "Cotización eliminada correctamente" });
    }
}