using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sora_AppProject.Application.UseCases.Cotizaciones.Commands;
using Sora_AppProject.Application.UseCases.Cotizaciones.Queries;

namespace Sora_AppProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CotizacionesController : ControllerBase
{
    private readonly IMediator _mediator;
    public CotizacionesController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult> GetAll() => Ok(await _mediator.Send(new GetAllCotizacionesQuery()));

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var res = await _mediator.Send(new GetCotizacionByIdQuery(id));
        return res != null ? Ok(res) : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] AddCotizacionCommand command) => Ok(await _mediator.Send(command));

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateCotizacionCommand command)
    {
        if (id != command.Id) return BadRequest();
        return await _mediator.Send(command) ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id) => await _mediator.Send(new DeleteCotizacionCommand(id)) ? NoContent() : NotFound();
}
