using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sora_AppProject.Application.Features.Cotizaciones.Commands;
using Sora_AppProject.Application.Features.Cotizaciones.Queries;

namespace Sora_AppProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CotizacionesController : ControllerBase
{
    private readonly IMediator _mediator;
    public CotizacionesController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult> GetAll() => Ok(await _mediator.Send(new GetAllCotizacionesQuery()));

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreateCotizacionCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteCotizacionCommand(id));
        return result ? NoContent() : NotFound();
    }
}
