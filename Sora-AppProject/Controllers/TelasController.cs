using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sora_AppProject.Application.UseCases.Telas.Commands;
using Sora_AppProject.Application.UseCases.Telas.Queries;

namespace Sora_AppProject.Controllers;

[ApiController]
[Route("api/telas")]
[Authorize]
public class TelasController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var result = await mediator.Send(new GetAllTelasQuery(pageNumber, pageSize));
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await mediator.Send(new GetTelaByIdQuery(id));
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "taller,admin")]
    public async Task<IActionResult> Add([FromBody] AddTelaCommand command)
    {
        await mediator.Send(command);
        return Ok(new { message = "Tela creada correctamente" });
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "taller,admin")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTelaCommand command)
    {
        command.Id = id;
        await mediator.Send(command);
        return Ok(new { message = "Tela actualizada correctamente" });
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await mediator.Send(new DeleteTelaCommand(id));
        return Ok(new { message = "Tela eliminada correctamente" });
    }
}