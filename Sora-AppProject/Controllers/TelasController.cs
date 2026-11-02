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

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CreateTelaCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteTelaCommand(id));
        return result ? NoContent() : NotFound();
    }
}
