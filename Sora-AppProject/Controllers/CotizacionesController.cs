using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sora_AppProject.Application.Dtos;
using Sora_AppProject.Application.UseCases.Cotizaciones.Commands;
using Sora_AppProject.Application.UseCases.Cotizaciones.Queries;

namespace Sora_AppProject.Controllers;

[ApiController]
[Route("api/cotizaciones")]
[Authorize]
public class CotizacionesController : ControllerBase
{
    private readonly IMediator _mediator;

    public CotizacionesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _mediator.Send(new GetAllCotizacionesQuery(pageNumber, pageSize));
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetCotizacionByIdQuery(id));
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "taller,admin")]
    public async Task<IActionResult> Add([FromBody] CotizacionDto request)
    {
        var id = await _mediator.Send(new AddCotizacionCommand(request));
        return Ok(new { id, message = "Cotización creada correctamente" });
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "taller,admin")]
    public async Task<IActionResult> Update(int id, [FromBody] CotizacionDto request)
    {
        await _mediator.Send(new UpdateCotizacionCommand(id, request));
        return Ok(new { message = "Cotización actualizada correctamente" });
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteCotizacionCommand(id));
        return Ok(new { message = "Cotización eliminada correctamente" });
    }
}
