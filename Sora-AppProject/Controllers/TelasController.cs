using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sora_AppProject.Application.UseCases.Telas.Commands;
using Sora_AppProject.Application.UseCases.Telas.Queries;

namespace Sora_AppProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TelasController : ControllerBase
{
    private readonly IMediator _mediator;
    public TelasController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult> GetAll() => Ok(await _mediator.Send(new GetAllTelasQuery()));

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var res = await _mediator.Send(new GetTelaByIdQuery(id));
        return res != null ? Ok(res) : NotFound();
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] AddTelaCommand command) => Ok(await _mediator.Send(command));

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateTelaCommand command)
    {
        if (id != command.Id) return BadRequest();
        return await _mediator.Send(command) ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id) => await _mediator.Send(new DeleteTelaCommand(id)) ? NoContent() : NotFound();
}
