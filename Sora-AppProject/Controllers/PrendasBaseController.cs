using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sora_AppProject.Application.UseCases.PrendasBase.Commands;
using Sora_AppProject.Application.UseCases.PrendasBase.Queries;

namespace Sora_AppProject.Controllers;

[ApiController]
[Route("api/prendas-base")]
[Authorize]
public class PrendasBaseController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var result = await mediator.Send(new GetAllPrendasBaseQuery(pageNumber, pageSize));
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await mediator.Send(new GetPrendaBaseByIdQuery(id));
        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Add([FromBody] AddPrendaBaseCommand command)
    {
        await mediator.Send(command);
        return Ok(new { message = "Prenda base creada correctamente" });
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePrendaBaseCommand command)
    {
        command.Id = id;
        await mediator.Send(command);
        return Ok(new { message = "Prenda base actualizada correctamente" });
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await mediator.Send(new DeletePrendaBaseCommand(id));
        return Ok(new { message = "Prenda base eliminada correctamente" });
    }
}